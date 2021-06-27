using System.Collections.Generic;
using DataAccess.Models.Dtos;
using DataAccess.Sql;

namespace DataAccess.BusinessLogic
{
    public class CardTypeProcessor
    {
        private readonly string _connectionString;

        public CardTypeProcessor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<UserCardDto> GetUserCards()
        {
            string sql = @"SELECT uc.UserCardID, us.FirstName, us.LastName, ca.CardNumber, ca.ValidThru, ct.[Type], po.OrganizationName
                            FROM dbo.User_Card uc
                            INNER JOIN dbo.Cards ca ON uc.CardID = ca.CardID
                            INNER JOIN dbo.CardTypes ct ON ca.CardTypeID=ct.CardTypeID
                            INNER JOIN dbo.PaymentOrganizations po ON ca.PaymentOrganizationID=po.PaymentOrganizationID
                            INNER JOIN dbo.Users us ON uc.UserID = us.UserID;";

            return SqlDataAccess.LoadData<UserCardDto>(sql, _connectionString);
        }

        public int InsertUserCard(UserCardBaseDto userCard)
        {
            string sql = rollBackOnError(@"DECLARE @CardID [int];
                            DECLARE @UserID [int];

                            INSERT INTO CardsTaskDB.dbo.Cards values (@CardNumber, @ValidThru, 
	                            (SELECT ct.CardTypeID FROM CardsTaskDB.dbo.CardTypes ct WHERE ct.Type = @Type), 
	                            (SELECT po.PaymentOrganizationID FROM CardsTaskDB.dbo.PaymentOrganizations po WHERE po.OrganizationName = @OrganizationName)) SET @CardID = SCOPE_IDENTITY();

                            INSERT INTO CardsTaskDB.dbo.Users values(@FirstName, @LastName) SET @UserID = SCOPE_IDENTITY();

                            INSERT INTO CardsTaskDB.dbo.User_Card values
	                            (@CardID,@UserID);");

            return SqlDataAccess.SaveData(sql, userCard, _connectionString);
        }

        public int UpdateUserLastName(UserCardBaseDto userCard, int userCardId)
        {
            string sql = rollBackOnError(@$"DECLARE @UserID [int];
                    SET @UserID=(SELECT uc.UserID FROM CardsTaskDB.dbo.User_Card uc WHERE uc.UserCardID={userCardId});
                    UPDATE CardsTaskDB.dbo.Users SET Users.LastName=@LastName FROM CardsTaskDB.dbo.Users WHERE Users.UserID=@UserID;");

            return SqlDataAccess.SaveData(sql, userCard, _connectionString);
        }

        public int DeleteUserCardWithCard(int userCardId)
        {
            string sql = rollBackOnError(@$"DECLARE @CardID [int];
	                            SET @CardID=(SELECT uc.CardID FROM CardsTaskDB.dbo.User_Card uc WHERE uc.UserCardID={userCardId});

	                            DELETE FROM CardsTaskDB.dbo.User_Card WHERE User_Card.UserCardID={userCardId};
	                            DELETE FROM CardsTaskDB.dbo.Cards WHERE CardID=@CardID;");

            return SqlDataAccess.RemoveData(sql, _connectionString);
        }

        private string rollBackOnError(string sql)
        {
            return $@"
                    BEGIN TRANSACTION
                    BEGIN TRY
                    {sql}
                    COMMIT
                    END TRY
                    BEGIN CATCH
	                    SELECT 
		                    ERROR_MESSAGE() AS ErrorMessage,
		                    ERROR_NUMBER() AS ErrorNumber
                        ROLLBACK TRANSACTION
                    END CATCH";
        }
    }
}
