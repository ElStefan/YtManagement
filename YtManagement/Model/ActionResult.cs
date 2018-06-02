using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YtManagement.Model
{
    public class ActionResult
    {
        public ActionStatus Status { get; set; }
        public string Message { get; set; }
        public ActionResult()
        {

        }
        public ActionResult(ActionResult item)
        {
            this.Status = item.Status;
            this.Message = item.Message;
        }

        public ActionResult(ActionStatus status)
        {
            this.Status = status;
        }

        public ActionResult(ActionStatus status, string message)
        {

            this.Status = status;
            this.Message = message;
        }
    }
    public class ActionResult<T> : ActionResult
    {
        public T Data { get; set; }

        public ActionResult() : base()
        {

        }
        public ActionResult(ActionResult item) : base(item)
        {

        }
        public ActionResult(ActionStatus status) : base(status)
        {
        }
        public ActionResult(ActionStatus status, T data) : base(status)
        {
            this.Data = data;
        }
        public ActionResult(ActionStatus status, string message) : base(status,message)
        {
        }

    }
}
