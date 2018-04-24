using System;
using AE.Net.Mail;

namespace DrDenese.Helpers
{
    public class EmailReader
    {
        public MailMessage GetLastEmail(string username, string password = "newuser123")
        {
            using (var imap = new ImapClient("imap.gmail.com", username, password, port: 993, secure: true, skipSslValidation: true))
            {
                var msgs = imap.SearchMessages(
                    SearchCondition.Unseen().And(
                        SearchCondition.From("info@beeptify.com"),
                        SearchCondition.SentSince(DateTime.Now.Subtract(TimeSpan.FromMinutes(20)))
                        ));

                return msgs.Length > 0 ? msgs[0].Value : null;
            }
        }

        public void MarkAsSeen(string username, MailMessage message, string password = "newuser123")
        {
            using (var imap = new ImapClient("imap.gmail.com", username, password, port: 993, secure: true, skipSslValidation: true))
            {
                imap.SetFlags(Flags.Seen, message);
            }
        }

        public void MarkAllAsSeen(string username, string password = "newuser123")
        {
            using (var imap = new ImapClient("imap.gmail.com", username, password, port: 993, secure: true, skipSslValidation: true))
            {
                var msgs = imap.SearchMessages(
                    SearchCondition.Unseen().And(
                        SearchCondition.From("info@beeptify.com")
                        ));

                foreach (var msg in msgs)
                {
                    imap.SetFlags(Flags.Seen, msg.Value);
                }
            }
        }
    }
}
