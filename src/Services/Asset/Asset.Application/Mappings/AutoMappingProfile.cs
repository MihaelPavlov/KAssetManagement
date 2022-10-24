namespace Asset.Application.Mappings
{
    using Asset.Application.Interfaces;
    using AutoMapper;
    using System.Reflection;

    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile(Type objectType)
        {
            var assembly = Assembly.GetAssembly(objectType);

            if (assembly == null)
                throw new Exception($"Valid type {objectType} is expected");

            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }

}
