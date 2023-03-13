namespace MilibooTests {
    [TestMethod]
    public void Postutilisateur_ModelValidated_CreationOK_AvecMoq() {
        // Arrange
        var mockRepository = new Mock<IDataRepository<Utilisateur>>();
        var userController = new UtilisateursController(mockRepository.Object);
        Utilisateur user = new Utilisateur {
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
        var actionResult = userController.PostUtilisateur(user).Result;
        // Assert
        Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
        Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
        var result = actionResult.Result as CreatedAtActionResult;
        Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
        user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
        Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
    }

    [TestMethod]
    public void 
}