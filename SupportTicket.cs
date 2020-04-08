using System;
using System.Collections.Generic;
using System.Text;

namespace ticketSupport
{
    public class SupportTicket : IComparable<SupportTicket>
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; } //nullable 

        public string Number { get; set; }

        private string _content;

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value == null || value.Length < 10)
                    throw new Exception("Ticket Support Content is required and it should be at least 10 digts");

                _content = value;
            }
        }

        public Employee Requester { get; set; }

        public TicketSupportStatus Status { get; set; } = TicketSupportStatus.Open;

        public Employee AssignedTo { get; set; }

        public TicketSupportLevel? Level { get; set; } //nullable

        public List<string> Attachments { get; set; } = new List<string>();

        public Priority Priority { get; set; } 

        public Department Department { get; set; }

        public SupportTicket(string content, 
            Employee requester, 
            List<string> attachments, 
            Department department)
        {
            //Default Values
            CreatedDate = DateTime.Now;
            Number = "20200403-0001";
            Status = TicketSupportStatus.Open;

            //User Values
            Content = content;
            if (requester == null)
                throw new Exception("Ticket Support Must have Requester.");

            Requester = requester;

            if(department == null)
                throw new Exception("Ticket Support Must have Department.");

            Department = department;

            if(attachments != null)
            {
                if(attachments.Count > 5)
                    throw new Exception("Attachment should not be more than 5");

                Attachments.AddRange(attachments);
            }
        }


        public string Assgin(Employee assignedTo, TicketSupportLevel level, Priority priority)
        {
            
            string isAssigned = "";

            if (Status == TicketSupportStatus.Closed)
                return "status you can not assign closed support ticket";

                if(Level != null)
                 if (Math.Abs((int)level - (int)Level) > 1)
                 return " level You can not upgrade/downgrade more than one level at a time. /n try again";
           
            if (assignedTo.Department != Department)
                return "employee You must assign an employee with same department as ticket";
          
            if (assignedTo.listOfTickets.Count == Employee.maxNumberOfTickets)
                return "employee can not assign new to ticket to " + assignedTo.Name;
             
            if (isAssigned == "")
            {
                Level = level;
                Priority = priority;
                assignedTo.listOfTickets.Add(this);
                assignedTo.listOfTickets.Sort();
                AssignedTo = assignedTo;
                ModifiedDate = DateTime.Now;
            }
           
            return isAssigned;
        }

        public int CompareTo(SupportTicket ticket)
        {
            if (Status.Equals(TicketSupportStatus.Pending))
                return 1;

            if (Priority.Order < ticket.Priority.Order)
                return 1;
            else if (Priority.Order > ticket.Priority.Order)
                return -1;
            else
            {
                if(ModifiedDate == null && ticket.ModifiedDate == null)
                {
                    if (CreatedDate < ticket.CreatedDate)
                        return 1;
                    else if (CreatedDate > ticket.CreatedDate)
                        return -1;
                    else
                        return 0;
                }
                else if (ModifiedDate == null)
                {
                    if (CreatedDate < ticket.ModifiedDate)
                        return -1;
                    else if (CreatedDate > ticket.ModifiedDate)
                        return 1;
                    else
                        return 0;
                }
                else 
                {
                    if (ModifiedDate < ticket.CreatedDate)
                        return -1;
                    else if (ModifiedDate > ticket.CreatedDate)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        public string close()
        {
            if (Level != TicketSupportLevel.Level1)
                return "you can not close a ticket with level greater than 1";
            
            if (Status != TicketSupportStatus.Active)
                return "you can not close  a ticket that is not active ";

            Status = TicketSupportStatus.Closed;
                return "ticket closed";

        }
    }
}
