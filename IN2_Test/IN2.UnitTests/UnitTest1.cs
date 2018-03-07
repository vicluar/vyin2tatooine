using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Tatooine.Domain.Common;
using Tatooine.WebUI;
using System.IO;
using Tatooine.Domain.Entities;
using Tatooine.Domain.Concrete;
using System.Data.Entity;

namespace IN2.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            Database.SetInitializer<EFDbContext>(null);
        }

        [TestMethod]
        public void Can_Create_And_Write_Log()
        {
            // arrange
            string logPath = @"V:\";
            string message = "Exception in application";

            // act
            OutputFileLog.Instance.SetMessageLogging(logPath, message);
            string line = File.ReadLines(string.Format("{0}{1}", logPath, "20150618.log")).FirstOrDefault();

            // assert
            Assert.IsTrue(File.Exists(string.Format("{0}{1}", logPath, "20150618.log")));
            Assert.IsTrue(line.Contains(message));
        }

        [TestMethod]
        public void Can_Add_Citizen()
        {
            // arrange
            Citizen citizen = new Citizen
            {
                Name = "Darth Vader",
                SpecieType = "Chief",
                RoleID = 3,
                CitizenStatusID = 1
            };

            // act
            EFCitizenRepository citizenRepository = new EFCitizenRepository();
            citizenRepository.SaveCitizen(citizen);

            // assert
            Assert.IsTrue(citizenRepository.Citizens.Any(c => c.Name == "Darth Vader"));
        }

        [TestMethod]
        public void Can_Add_Role()
        {
            // arrange
            Role role = new Role 
            { 
                Description = "Unit Test Role"
            };

            // act
            EFRoleRepository roleRepository = new EFRoleRepository();
            roleRepository.SaveRole(role);

            // assert
            Assert.IsTrue(roleRepository.Roles.Any(r => r.Description == "Unit Test Role"));
        }

        [TestMethod]
        public void Can_Return_Citizen()
        {
            // arrange
            int citizenID = 1;
            string citizenName = "Vicluar";

            // act
            EFCitizenRepository citizenRepository = new EFCitizenRepository();
            Citizen citizen = citizenRepository.GetCitizen(citizenID);

            // assert
            Assert.AreEqual(citizenID, citizen.ID);
            Assert.AreEqual(citizenName, citizen.Name);
        }

        [TestMethod]
        public void Can_Create_Text_File()
        {
            // arrange
            string logPath = @"V:\";
            string message = "rebeld (name) on (planet) at (datetime)";

            // act
            OutputFileLog.Instance.SetMessageToFile(logPath, "rebeld.log", message);
            string line = File.ReadLines(string.Format("{0}{1}", logPath, "rebeld.log")).FirstOrDefault();

            // assert
            Assert.IsTrue(File.Exists(string.Format("{0}{1}", logPath, "rebeld.log")));
            Assert.IsTrue(line.Contains(message));
        }
    }
}
