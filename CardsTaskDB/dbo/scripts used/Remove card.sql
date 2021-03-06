BEGIN TRANSACTION
BEGIN TRY
	DELETE FROM CardsTaskDB.dbo.Cards WHERE CardID=(SELECT uc.CardID FROM CardsTaskDB.dbo.User_Card uc WHERE uc.UserCardID=2)
	DELETE FROM CardsTaskDB.dbo.User_Card WHERE User_Card.CardID=2;
END TRY
BEGIN CATCH
	SELECT 
		ERROR_MESSAGE() AS ErrorMessage,
		ERROR_NUMBER() AS ErrorNumber
    ROLLBACK TRANSACTION
END CATCH