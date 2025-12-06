namespace PublisherSubscriber
{
    // Publisher
    public class NewsPublisher
    {
        // The event that subscribers will listen to
        //public event Action<string>? NewsPublished;
        public event EventHandler<string>? NewsPublished;

        public void Publish(string message)
        {
            Console.WriteLine($"\n[Publisher] Publishing message: {message}");
            NewsPublished?.Invoke(this, message); // Notify all subscribers  
        }
    }

    // Subscribers
    // Email Subscriber
    public class EmailSubscriber
    {
        public void OnNewsReceived(object? sender, string message)
        {
            Console.WriteLine($"[Email] Sending email with news: {message}");
        }
    }

    // SMS Subscriber
    public class SmsSubscriber
    {
        public void OnNewsReceived(object? sender, string message)
        {
            Console.WriteLine($"[SMS] Sending SMS with news: {message}");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // create publisher
            var publisher = new NewsPublisher();
            
            // create subscribers
            var email = new EmailSubscriber();
            var sms = new SmsSubscriber();

            // subscribe "subscriber: hay publisher please subscribe me"
            publisher.NewsPublished += email.OnNewsReceived;
            publisher.NewsPublished += sms.OnNewsReceived;

            // publish first message
            publisher.Publish("first message");

            // unsubscibe sms and republish
            publisher.NewsPublished -= sms.OnNewsReceived;
            publisher.Publish("second message");


            // Notes
            // publisher.NewsPublished = null; // not allowed event restriction (if it was delegate it would be allowed)
            // publisher.NewsPublished(); // cann't invoke the event outside the class (event restriction over delegate)
        }
    }
}
