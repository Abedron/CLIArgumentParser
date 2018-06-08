using System;
using System.Reflection;

namespace CLI
{
    public class CLIArgumentParser<T> where T : new()
    {
        public CaseType CaseType => _caseType;
        public char NameSeparator => _nameSeparator;
        public char ValueSeparator => _valueSeparator;
        
        private readonly CaseType _caseType;
        private readonly char _nameSeparator;
        private readonly char _valueSeparator;
        private readonly Capitalization _capital;

        public CLIArgumentParser(CaseType caseType, char nameSeparator = '-', char valueSeparator = '=')
        {
            _nameSeparator = nameSeparator;
            _valueSeparator = valueSeparator;
            _caseType = caseType;
            
            _capital = new Capitalization(caseType, nameSeparator);
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
                string[] parameters = argument.Split(_valueSeparator);

                if (parameters.Length > 1)
                {
                    string name = _capital.Change(parameters[0]);
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
