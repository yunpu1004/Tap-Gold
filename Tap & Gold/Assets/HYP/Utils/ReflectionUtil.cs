using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

public class ReflectionUtil
{
    public static Type GetCurrentType()
    {
        StackFrame frame = new StackFrame(1);
        var method = frame.GetMethod();
        var type = method.DeclaringType;
        return type;
    }

    public static string GetCurrentMethod()
    {
        StackFrame frame = new StackFrame(1);
        var method = frame.GetMethod();
        var type = method.DeclaringType;
        return type.Name + "." + method.Name;
    }

    public static Type[] GetAllSubClass(Type type)
    {
        var types = new List<Type>();

        foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach(var assemblyType in assembly.GetTypes())
            {
                if(assemblyType.IsClass && assemblyType.IsSubclassOf(type) && !assemblyType.IsAbstract)
                {
                    types.Add(assemblyType);
                }
                else if(assemblyType.BaseType is { IsGenericType: true } && assemblyType.BaseType.GetGenericTypeDefinition() == type)
                {
                    types.Add(assemblyType);
                }
            }
        }
        return types.ToArray();
    }

    public static Type[] GetAllStruct(params Type[] interfaces)
    {
        foreach(var type in interfaces)
        {
            if(!type.IsInterface)
            {
                throw new Exception($"Type {type} is not interface");
            }
        }

        var types = new List<Type>();
        
        foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach(var assemblyType in assembly.GetTypes())
            {
                bool add = true;
                
                if(!assemblyType.IsValueType) add = false;
                if(assemblyType.IsEnum) add = false;
                if(assemblyType.IsPrimitive) add = false;

                foreach(var interfaceType in interfaces)
                {
                    if(!interfaceType.IsAssignableFrom(assemblyType)) add = false;
                }

                if(add) types.Add(assemblyType);
            }
        }

        return types.ToArray();
    }

    public static object CreateInstance(Type type)
    {
        return Activator.CreateInstance(type);
    }

    public static MethodInfo[] GetStaticMethodsWithAttribute<T>() where T : Attribute
    {
        var methods = new List<MethodInfo>();

        foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach(var assemblyType in assembly.GetTypes())
            {
                foreach(var method in assemblyType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if(method.GetCustomAttribute<T>() != null)
                    {
                        methods.Add(method);
                    }
                }
            }
        }

        return methods.ToArray();
    }
}   
