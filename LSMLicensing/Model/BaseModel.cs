using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace LicenseRegistrationAPI.Model
{
    public enum NotificationType 
    {
        Error,
        Warning,
        Info,
        Success
    }
    public class Notification 
    {
        public Notification(NotificationType type, string message) 
        {
            Type = type;
            Message = message;
        }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
    }
    public abstract class BaseModel
    {
        public BaseModel() 
        {
            Notifications = new List<Notification>();
        }
        protected virtual IValidator _validator { get; }

        public void Validate() 
        {
            var validationResult = _validator.Validate(this);
            foreach (var error in validationResult.Errors)
            {
                Notifications.Add(new Notification(NotificationType.Error, error.ErrorMessage));
            }
        }
        
        public List<Notification> Notifications { get; private set; }

        public bool IsValid {
            get {
                return Notifications.All(x => x.Type != NotificationType.Error);
            }
        }
    }
}
