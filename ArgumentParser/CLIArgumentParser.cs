using System;
using System.Reflection;

namespace ArgumentParser
{
    public class CLIArgumentParser<T> where T : new()
    {
        public char ValueSeparator { get; }

        public CLIArgumentParser(char valueSeparator = '=')
        {
            ValueSeparator = valueSeparator;
        }

        public T Parse()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            T t = Parse(arguments);
            return t;
        }

        public T Parse(string[] arguments)
        {
            T t = new T();

            foreach (string argument in arguments)
            {
                string[] parameters = argument.Split(ValueSeparator);

                if (parameters.Length > 1)
                {
                    string name = parameters[0];
                    string value = parameters[1];
                    
                    FieldInfo field = t.GetType().GetField(name);
                    if (field == null)
                        continue;

                    object typedValue = Convert.ChangeType(value, field.FieldType);
                    object boxed = t;
                    field.SetValue(boxed, typedValue);
                    t = (T)boxed;
                }
            }
            
            return t;
        }
    }
}
