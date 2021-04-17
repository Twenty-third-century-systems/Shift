namespace Cabinet.Dtos.Internal.Request {
    public class QueryRequestDto {
        public int ApplicationId { get; set; }
        public QryRequestDto Query { get; set; }
    }
}