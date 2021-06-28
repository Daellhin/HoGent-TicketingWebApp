using _2021_dotnet_g_04.Controllers;
using _2021_dotnet_g_04.Models.Domain.Enumerations;
using _2021_dotnet_g_04.Models.Domain.Repositories;
using _2021_dotnet_g_04.Models.ViewModels;
using _2021_dotnet_g_04.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _2021_dotnet_g_04.Tests.Controllers
{
    public class ContractControllerTest
    {
        private readonly DummyDbContext _dummyContext;
        private readonly ContractController _contractController;
        private readonly ContractCreateViewModel _contractViewModelDatNietOverlapt;
        private readonly Mock<IContractTypeRepository> _mockContractTypeRepository;

        public ContractControllerTest()
        {
            _dummyContext = new DummyDbContext();
            _mockContractTypeRepository = new Mock<IContractTypeRepository>();
            _contractController = new ContractController(_mockContractTypeRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };

            _contractViewModelDatNietOverlapt = new ContractCreateViewModel
            {
                Startdatum = _dummyContext.Contract1.Einddatum.Value.AddDays(1),
                ContractTypeId = 1
            };
        }

        #region == Index ==
        [Fact]
        public void Index_PassesAllRunningContractsOfCustomerSortedByNumber()
        {
            var result = Assert.IsType<ViewResult>(_contractController.Index(_dummyContext.Klant1));
            var contracts = Assert.IsType<List<ContractViewModel>>(result.Model);
            Assert.Equal(2, contracts.Count());
            Assert.Equal(1, contracts[0].Nummer);
            Assert.Equal(2, contracts[1].Nummer);
            Assert.Equal(true, result.ViewData["OpenContracts"]);
            Assert.Equal("OpenContracts", result.ViewData["activeLink"]);
        }
        #endregion

        #region == ClosedContracts ==
        [Fact]
        public void ClosedContracts_PassesAllClosedContractsOfCustomerSortedByNumber()
        {
            var result = Assert.IsType<ViewResult>(_contractController.ClosedContracts(_dummyContext.Klant1));
            var contracts = Assert.IsType<List<ContractViewModel>>(result.Model);
            Assert.Single(contracts);
            Assert.Equal(3, contracts[0].Nummer);
            Assert.Equal(false, result.ViewData["OpenContracts"]);
            Assert.Equal("ClosedContracts", result.ViewData["activeLink"]);
        }
        #endregion

        #region == Create Get==
        [Fact]
        public void CreateGet_HaaltAlleActieveContractTypesOp()
        {
            _mockContractTypeRepository.Setup(e => e.GetAllActiveContractTypes()).Returns(_dummyContext.ContractTypes.Where(e => e.Status == ContractTypeStatus.Active).ToList());

            var result = Assert.IsType<ViewResult>(_contractController.Create());
            var model = Assert.IsType<ContractCreateViewModel>(result.Model);
            var contractTypes = Assert.IsType<List<ContractTypeViewModel>>(result.ViewData["contractTypes"]);
            Assert.Equal(2, contractTypes.Count());
            Assert.Equal("CreateContract", result.ViewData["activeLink"]);

            _mockContractTypeRepository.Verify(e => e.GetAllActiveContractTypes(), Times.Once);
        }
        #endregion

        #region == Create Post==
        [Fact]
        public void CreatePost_GeldigeGegevens_VoegtContractToeAanKlant()
        {
            _mockContractTypeRepository.Setup(e => e.GetBy(1)).Returns(_dummyContext.ContractType1);
            _mockContractTypeRepository.Setup(e => e.GetAllActiveContractTypes()).Returns(_dummyContext.ContractTypes.Where(e => e.Status == ContractTypeStatus.Active).ToList());
            int aantalContracten = _dummyContext.Klant1.Contracten.Count();
            _contractController.Create(_contractViewModelDatNietOverlapt, _dummyContext.Klant1);

            Assert.Equal(aantalContracten + 1, _dummyContext.Klant1.Contracten.Count());
            _mockContractTypeRepository.Verify(e => e.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreatePost_GeldigeGegevens_RedirectsNaarIndex()
        {
            _mockContractTypeRepository.Setup(e => e.GetAllActiveContractTypes()).Returns(_dummyContext.ContractTypes.Where(e => e.Status == ContractTypeStatus.Active).ToList());
            _mockContractTypeRepository.Setup(e => e.GetBy(1)).Returns(_dummyContext.ContractType1);

            var result = Assert.IsType<RedirectToActionResult>(_contractController.Create(_contractViewModelDatNietOverlapt, _dummyContext.Klant1));
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void CreatePost_OngeldigeModelState_RetourneertModel()
        {
            _contractController.ModelState.AddModelError("any key", "any error");
            _mockContractTypeRepository.Setup(e => e.GetBy(1)).Returns(_dummyContext.ContractType1);
            _mockContractTypeRepository.Setup(e => e.GetAllActiveContractTypes()).Returns(_dummyContext.ContractTypes.Where(e => e.Status == ContractTypeStatus.Active).ToList());

            ViewResult result = Assert.IsType<ViewResult>(_contractController.Create(_contractViewModelDatNietOverlapt, _dummyContext.Klant1));
            Assert.Null(result.ViewName);
            Assert.Equal(_contractViewModelDatNietOverlapt, result.Model);
        }
        #endregion

        #region == Cancel ==
        [Fact]
        public void Cancel_SteltStatusInOpCancelledEnSteltEindDatumIn()
        {
            _contractController.Cancel(_dummyContext.Contract1.Nummer, _dummyContext.Klant1);
            Assert.Equal(ContractStatus.Cancelled, _dummyContext.Contract1.Status);
            Assert.Equal(DateTime.Today.Day, _dummyContext.Contract1.Einddatum.Value.Day);
            _mockContractTypeRepository.Verify(e => e.SaveChanges(), Times.Once);
        }
        #endregion

        #region == Details ==
        [Fact]
        public void Details_HaaltJuisteContractEnContractTypeOp()
        {
            var result = Assert.IsType<ViewResult>(_contractController.Details(1, _dummyContext.Klant1));
            var model = Assert.IsType<ContractViewModel>(result.Model);

            Assert.Equal(1, model.Nummer);
            Assert.Equal(_dummyContext.ContractType1.Id, model.ContractType.Id);
        }
        #endregion
    }
}
