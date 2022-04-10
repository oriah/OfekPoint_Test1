using System.ComponentModel;
using Sisma.Project1.Shared.Helpers;

namespace Sisma.Project1.Shared.Exceptions
{

    /// <summary>
    /// An enumerator for specifying various known exception types for the current application.
    /// </summary>
    public enum SismaExceptionTypes
    {
        /// <summary>
        /// Specifies that no value was set.
        /// </summary>
        Unspecified = 0,
        /// <summary>
        /// A general exception
        /// </summary>
        General = 1,
        ObjectDependencyExists = 2,
        StudentSchoolMismatch = 3,
        /// <summary>
        /// General: An object of the given id already exists.
        /// </summary>
        [Description("An object of the given id already exists")]
        ObjectAlreadyExists = 998,
        /// <summary>
        /// General: The requested object or entity cannot be found.
        /// </summary>
        [Description("The requested object or entity cannot be found")]
        ObjectNotFound = 999,
    }

    /// <summary>
    /// The exception that is thrown in the current application level.
    /// </summary>
    public class SismaException : Exception
    {
        public SismaExceptionTypes Type { get; set; }

        public SismaException(string message) :
          base(message)
        {
            Type = SismaExceptionTypes.General;
        }
        public SismaException(SismaExceptionTypes type) :
          base(type.ToDescriptionString())
        {
            this.Type = type;
        }

        public SismaException(SismaExceptionTypes type, string message) :
          base(message)
        {
            this.Type = type;
        }

        public SismaException(string message, Exception innerException) :
          base(message, innerException)
        {
            this.Type = SismaExceptionTypes.General;
        }
        public SismaException(SismaExceptionTypes type, string message, Exception innerException) :
          base(message, innerException)
        {
            this.Type = type;
        }
    }
}
