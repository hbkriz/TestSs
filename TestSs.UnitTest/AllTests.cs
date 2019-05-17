using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSs.UnitTest
{
    [TestClass]
    public class AllTests
    {
        private Repository<Storeable> _repository;
        private Storeable _firstItem;
        private Storeable _secondItem;
        private Storeable _thirdItem;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new Repository<Storeable>();
            SetupMock();
        }

        private void SetupMock()
        {
            _firstItem = new Storeable { Id = 1, Name = "ExistingItem" };
            _secondItem = new Storeable { Id = 2, Name = "SomeItem" };
            _thirdItem = new Storeable { Id = 3, Name = "NewItemToBeSaved" };
        }

        [TestMethod]
        public void All_Returns_IEnumberable()
        {
            var expected = _repository.All();
            Assert.IsInstanceOfType(expected, typeof(IEnumerable<Storeable>));
        }

        [TestMethod]
        public void All_Returns_Expected_Result()
        {
            _repository.Save(_firstItem);
            _repository.Save(_secondItem);
            _repository.Save(_thirdItem);
            var expected = _repository.All();
            Assert.IsTrue(expected.Count()==3);
        }

        [TestMethod]
        public void Save_NewItem()
        {
            _repository.Save(_firstItem);
            var expected = _repository.All();
            Assert.IsTrue(expected.Contains(_firstItem));
        }

        [TestMethod]
        public void Save_NewItem_Twice_Should_Delete()
        {
            _repository.Save(_firstItem);
            _repository.Save(_firstItem);
            var expected = _repository.All();
            Assert.IsTrue(expected.Count() == 1);
        }

        [TestMethod]
        public void Save_TwoNewItems()
        {
            _repository.Save(_firstItem);
            _repository.Save(_secondItem);
            var expected = _repository.All();
            Assert.IsTrue(expected.Count() == 2);
        }

        [TestMethod]
        public void Delete_Existing_Item()
        {
            _repository.Save(_firstItem);
            _repository.Delete(1);
            var expected = _repository.All();
            Assert.IsFalse(expected.Contains(_firstItem));
            Assert.IsTrue(expected.Count() == 0);
        }

        [TestMethod]
        public void Find_Item_By_Id()
        {
            
            _repository.Save(_secondItem);
            _repository.Save(_firstItem);
            _repository.Save(_thirdItem);
            var expected = _repository.FindById(2);
            Assert.AreEqual(_secondItem, expected);
        }


        [TestMethod]
        public void Find_Item_By_Id_NotFound()
        {
            _repository.Save(_firstItem);
            _repository.Save(_secondItem);
            var expected = _repository.FindById(3);
            Assert.IsTrue(expected == null);
        }
    }
}
