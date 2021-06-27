SELECT uc.UserCardID, us.FirstName, us.LastName, ca.CardNumber, ca.ValidThru, ct.[Type], po.OrganizationName
FROM dbo.User_Card uc
INNER JOIN dbo.Cards ca ON uc.CardID = ca.CardID
INNER JOIN dbo.CardTypes ct ON ca.CardTypeID=ct.CardTypeID
INNER JOIN dbo.PaymentOrganizations po ON ca.PaymentOrganizationID=po.PaymentOrganizationID
INNER JOIN dbo.Users us ON uc.UserID = us.UserID;