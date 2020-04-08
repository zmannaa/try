using System;
using System.Collections.Generic;

namespace ticketSupport
{
    class Program
    {
        static void Main(string[] args)
        {
            #region values

            #region departments
            List<Department> departments = new List<Department>()
            {
                new Department("IT"),
                new Department("HR"),
                new Department("Information Security")
            };
            #endregion

            #region employee
            List<Employee> employees = new List<Employee>()
            {
            new Employee("Hammad",
                new DateTime(2020, 1, 1),
                "0563319949",
                "Eng",
                "1003",
                departments[0]),

            new Employee("Ahmad",
                 new DateTime(2020, 1, 1),
                 "0563319933",
                 "HR Admin",
                 "1004",
                 departments[1]),

            new Employee("Ali",
                 new DateTime(2020, 1, 1),
                 "0563119933",
                 "Information Security Admin",
                 "1005",
                 departments[2])
        };

            #endregion

            #region priority
            List<Priority> priorities = new List<Priority>()
            {
                new Priority()
                {
                Name = "Normal",
                MaxDays = 30,
                Order = 1
                },
                 new Priority()
                 {
                Name = "Important",
                MaxDays = 10,
                Order = 2
                },
                new Priority()
                 {
                Name = "Urgent",
                MaxDays = 5,
                Order = 3
                },
                new Priority()
                  {
                Name = "His Excllency Office",
                MaxDays = 1,
                Order = 99999
                }
               };
            #endregion

            #region attachment 
            List<string> attachments = new List<string>()
            {
                "link1",
                "link2",
                "link3"
            };
            #endregion

            SupportTicket ticket2 = new SupportTicket("Bug In InfoSec", employees[1], attachments, departments[2]);
            #region sort
            SupportTicket ticket3 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket4 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket5 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket6 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket7 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket8 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket9 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            SupportTicket ticket10 = new SupportTicket("Bug In InfoSec", employees[1], null, departments[2]);
            
            
            ticket3.Priority = priorities[0];
            ticket4.Priority = priorities[1];
            ticket5.Priority = priorities[2];
            ticket6.Priority = priorities[3];
            ticket7.Priority = priorities[0];
            ticket8.Priority = priorities[0];
            ticket9.Priority = priorities[2];
            ticket10.Priority = priorities[3];

            ticket3.CreatedDate = new DateTime(2020,4,5);
            ticket6.CreatedDate = new DateTime(2020, 4, 3);
            ticket7.CreatedDate = new DateTime(2020, 4, 1);
           
            employees[1].listOfTickets.Add(ticket3);
            employees[1].listOfTickets.Add(ticket4);
            employees[1].listOfTickets.Add(ticket5);
            employees[1].listOfTickets.Add(ticket6);
            employees[1].listOfTickets.Add(ticket7);
            employees[1].listOfTickets.Add(ticket8);
            employees[1].listOfTickets.Add(ticket9);
            employees[1].listOfTickets.Add(ticket10);
            employees[1].listOfTickets.Sort();
            foreach (var item in employees[1].listOfTickets)
            {
                Console.WriteLine(item.Priority.Name + "   " + item.CreatedDate.Day);
            }
            #endregion

            #endregion
            string isAssigned = "";
            int employeeNumber = 0;
            int ticketNumber = 0 ;
            int priority = 0;
            do
            {
               if (isAssigned.Length == 0)
                {
                    Console.WriteLine("Enter Employee Number:\n Hammad ==> 1 \n Ahmed ==> 2 \n Ali ==> 3 ");
                    employeeNumber = Convert.ToInt32(Console.ReadLine())-1;

                    Console.WriteLine("Enter Ticket Support Level: 1 , 2 or 3");
                    ticketNumber = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter type of priority:\n normal ==>1 \n important ==>2 \n urgent ==>3 \n heo ==> 4");
                     priority  = Convert.ToInt32(Console.ReadLine())-1;

                    //isAssigned = ticket2.Assgin(ali, TicketSupportLevel.Level3, urgent);
                    isAssigned = ticket2.Assgin(employees[employeeNumber], (TicketSupportLevel)ticketNumber, priorities[priority]);
                }
                else
                {
                    var reason = isAssigned.Split(' ')[0];
                    Console.WriteLine(isAssigned.Substring(isAssigned.IndexOf(' ')));
                    switch (reason)
                    {
                        case "status":
                            isAssigned = "";
                            break;

                        case "level":
                            Console.WriteLine("Enter Ticket Support Level: 1 , 2 or 3");
                            ticketNumber = Convert.ToInt32(Console.ReadLine());
                            break;

                        case "employee":
                            Console.WriteLine("Enter Employee Number:\n Hammad ==> 1 \n Ahmed ==> 2 \n Ali ==> 3 ");
                            employeeNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                            break;
                    }
                    isAssigned = ticket2.Assgin(assignedTo: employees[employeeNumber], (TicketSupportLevel)ticketNumber, priorities[priority]);
                }
            }
            while (isAssigned.Length > 0);
            
            string message = ticket2.close();
            Console.WriteLine(message);

        }
    }
}