namespace MilibooTests {
    [TestMethod]
    public void Postutilisateur_ModelValidated_CreationOK_AvecMoq() {
        // Arrange
        var mockRepository = new Mock<IDataRepository<Utilisateur>>();
        var AccountController = new UtilisateursController(mockRepository.Object);
        Utilisateur Account = new Utilisateur {
            Nom = "POISSON",
            Prenom = "Pascal",
            Mobile = "1",
            Mail = "poisson@gmail.com",
            Pwd = "Toto12345678!",
            Rue = "Chemin de Bellevue",
            CodePostal = "74940",
            Ville = "Annecy-le-Vieux",
            Pays = "France",
            Latitude = null,
            Longitude = null
        };
        // Act
        var actionResult = AccountController.PostUtilisateur(Account).Result;
        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
        Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
        var result = actionResult.Result as CreatedAtActionResult;
        Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
        Account.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
        Assert.AreEqual(Account, (Utilisateur)result.Value, "Utilisateurs pas identiques");
    }

    [TestMethod]
    public void 
}