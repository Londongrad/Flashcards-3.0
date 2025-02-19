using System.ComponentModel;

namespace FlashcardsLiblary
{
    public static partial class PropertyDescriptorHelper
    {
        private static readonly Dictionary<Type, PropertyDescriptorCollection> types = new Dictionary<Type, PropertyDescriptorCollection>();

        private static PropertyDescriptorCollection GetProperties(Type targetType)
        {
            if (!types.TryGetValue(targetType, out var properties))
            {
                properties = TypeDescriptor.GetProperties(targetType);
                types.Add(targetType, properties);
            }
            return properties;
        }

        public static void SetValue<T>(this T target, string propertyName, object value)
            where T : class
        {
            GetProperties(target.GetType())[propertyName]!.SetValue(target, value);
        }

        public static void SetValue<T>(this T target, PropertyDescriptor propertyDescriptor, object value)
            where T : class
        {
            propertyDescriptor.SetValue(target, value);
        }

        public static object GetValue<T>(this T target, string propertyName)
            where T : class
        {
            return GetProperties(target.GetType())[propertyName]!.GetValue(target)!;
        }

        public static object GetValue<T>(this T target, PropertyDescriptor propertyDescriptor)
            where T : class
        {
            return propertyDescriptor.GetValue(target)!;
        }

        public static void AddValueChanged<T>(this T target, PropertyDescriptor propertyDescriptor, PropertyChangedEventHandler handler)
            where T : class
        {
            new target_handler(target, propertyDescriptor, handler);
        }

        private static readonly Func<PropertyDescriptor, object, EventHandler> GetHandlerList =
            (Func<PropertyDescriptor, object, EventHandler>)
            typeof(PropertyDescriptor)
            .GetMethod("GetValueList", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
            .CreateDelegate(typeof(Func<PropertyDescriptor, object, EventHandler>));

        public static void RemoveValueChanged<T>(this T target, PropertyDescriptor propertyDescriptor, PropertyChangedEventHandler handler)
            where T : class
        {
            var list = GetHandlerList(propertyDescriptor, target);
            foreach (var item in list.GetInvocationList().Cast<EventHandler>().ToArray())
            {
                if (item.Target is target_handler th)
                {
                    object source = th.targetReference.Target!;
                    if (source != null && ReferenceEquals(source, target) && th.handler == handler)
                    {
                        propertyDescriptor.RemoveValueChanged(th, th.eventHandler);
                        break;
                    }
                }
            }
            new target_handler(target, propertyDescriptor, handler);
        }

        private class target_handler
        {
            public WeakReference targetReference;
            public PropertyChangedEventHandler handler;
            public PropertyDescriptor propertyDescriptor;

            public readonly PropertyChangedEventArgs args;

            public readonly EventHandler eventHandler = (s, e) =>
                {
                    target_handler th = (target_handler)s!;
                    object source = th.targetReference.Target!;
                    if (source is null)
                    {
                        th.propertyDescriptor.RemoveValueChanged(th, th.eventHandler);
                    }
                    else
                    {
                        th.handler(source, th.args);
                    }
                };

            public target_handler(object target, PropertyDescriptor propertyDescriptor, PropertyChangedEventHandler handler)
            {
                targetReference = new WeakReference(target);
                this.handler = handler;
                this.propertyDescriptor = propertyDescriptor;

                args = new PropertyChangedEventArgs(propertyDescriptor.Name);

                propertyDescriptor.AddValueChanged(this, eventHandler);
            }
        }
    }
}