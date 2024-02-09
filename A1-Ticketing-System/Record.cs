namespace A1_Ticketing_System;

public class Record
{
    public static readonly String DISPLAY_HEADER = $"\n{"Ticket ID",-20} {"Summary",-50} {"Status",-10} " +
    $"{"Priority",-10} {"Submitter",-20} " +
    $"{"Assigned",-20} {"Watching",-40}";
    
    public static readonly String WRITE_HEADER = "TicketID,Summary,Status,Priority,Submitter,Assigned,Watching";
    public String ticketID { get; set; }
    public String summary  { get; set; }
    public String status { get; set; }
    public String priority { get; set; }
    public String submitter { get; set; }
    public String assigned { get; set; }
    public String watchers { get; set; }

    public Record(String[] data)
    {
        this.ticketID = data[0];
        this.summary = data[1];
        this.status = data[2];
        this.priority = data[3];
        this.submitter = data[4];
        this.assigned = data[5];
        this.watchers = data[6];
    }

    public Record(string ticketId, string summary, string status, string priority, string submitter, string assigned, string watchers)
    {
        ticketID = ticketId;
        this.summary = summary;
        this.status = status;
        this.priority = priority;
        this.submitter = submitter;
        this.assigned = assigned;
        this.watchers = watchers;
    }

    public String ToWrite()
    {
        return $"{this.ticketID},{this.summary},{this.status}," +
               $"{this.priority},{this.submitter}," +
               $"{this.assigned},{this.watchers}";
    }
    
    public String ToDisplay()
    {
        return $"{this.ticketID,-20} {this.summary,-50} {this.status,-10} " +
               $"{this.priority,-10} {this.submitter,-20} " +
               $"{this.assigned,-20} {this.watchers.Replace("|", ", "),-40}";
    }
}
