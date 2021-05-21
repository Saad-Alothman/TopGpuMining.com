using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using CreaDev.Framework.Core.Exceptions;
using Microsoft.AspNet.Identity;

namespace CreaDev.Framework.Core
{
    public static class Guard
    {
        public static void AgainstNotTheCurrentUser(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId ))
                throw new PermissionException();
            if (UserId != Thread.CurrentPrincipal.Identity.GetUserId())
                throw new PermissionException();
        }
        public static void AgainstType<TType>(object theObject, string message = "") where TType : class
        {
            
            AgainstNull<NullReferenceException>(theObject, "null");
            if (theObject as TType == null)
                Throw<InvalidCastException>(message);


        }
        public static void AgainstNull(object theObject, string message = "")
        {
            AgainstNull<NullReferenceException>(theObject, message);
        }

        private static void Throw<TException>(string message = "") where TException : Exception, new()
        {
            TException instance = (TException)Activator.CreateInstance(typeof(TException), new object[] { message });
            throw instance;
        }
        public static void AgainstNull<TException>(object theObject, string message = "") where TException : Exception, new()
        {

            if (theObject == null)
                Throw<TException>(message);
        }

        public static void AgainstFalse(bool boolValue, string message = "")
        {
            if (!boolValue)
                throw new BusinessException(message);
        }
        public static void AgainstFalse<TException>(bool boolValue, string message = "") where TException : Exception, new()
        {
            if (!boolValue)
                Throw<TException>(message);
        }
       
        public static void AgainstNotInAnyRole(params string[] roleNames)
        {

            if (!roleNames.Any(Thread.CurrentPrincipal.IsInRole))
                throw new PermissionException();
        }

      
        public static void AgainstNotInRoles(params string[] roleNames)
        {
            if (!roleNames.All(Thread.CurrentPrincipal.IsInRole))
                throw new PermissionException();

        }
        public static void AgainstNotInRoleOrRoles(string role,params string[] roleNames)
        {
            if (!roleNames.All(Thread.CurrentPrincipal.IsInRole) && !Thread.CurrentPrincipal.IsInRole(role))
                throw new PermissionException();

        }
    }

}
