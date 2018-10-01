namespace Bizland.Core
{
    public interface IPushLocalNotification
    {
        void PushLocalNotification(string title, string content, string icon);
    }
}
