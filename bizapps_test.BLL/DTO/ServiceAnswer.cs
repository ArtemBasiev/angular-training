namespace bizapps_test.BLL.DTO
{
    public class ServiceAnswer<T>
    {
        public ServiceAnswer(T entityToReceive, AnswerStatus status)
        {
            ReceivedEntity = entityToReceive;
            Status = status;
        }

        public T ReceivedEntity { get; internal set; }

        public AnswerStatus Status { get; private set; }
    }


    public enum AnswerStatus
    {
        Failed,
        Successfull
    }
}