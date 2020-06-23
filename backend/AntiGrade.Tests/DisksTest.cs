// namespace UnitTests
// {
//     public class UserTests : DatabaseBaseTest
//     {
//         IUnitOfWork _unitOfWork;
//         IMapper _mapper;
//         protected override async Task SeedAsync()
//         {
//             await TDbContext.SaveChangesAsync();
//         }
//         public DisksTest()
//         {
//             _unitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
//             _service = ServiceProvider.GetRequiredService<IDiskService>();
//             _controller = ServiceProvider.GetRequiredService<DiskController>();
//             _controller = ServiceProvider.GetRequiredService<DiskController>();
//             _mapper = ServiceProvider.GetRequiredService<IMapper>();

//         }
//         // GetAllDisks Tests
//         [Fact]
//         public void GetAllDisks_WhenCalled_ReturnsOkResult()
//         {

//             // Act
//             var okResult = _controller.GetAllDisks(null);
//             // Assert
//             Assert.IsType<OkObjectResult>(okResult.Result);
//         }

//         [Fact]
//         public void GetAllDisks_WhenCalled_ReturnsAllItems()
//         {
//             var props = new DbCallProps{
//                 Page =1,
//                 PageSize = 3,
//                 SortField = "Name",
//                 FilterProps = new List<FilterProps>()
//             };
//             props.FilterProps.Add(new FilterProps{
//                 FilterRroperty ="Price",
//                 FilterValue = 2000,
//                 FilterSign = Operators.LessOrEqual
//             });
//             props.FilterProps.Add(new FilterProps{
//                 FilterRroperty ="Name",
//                 FilterValue = "8",
//                 FilterSign = Operators.Contains
//             });
//             // Act
//             var okResult = _controller.GetAllDisks(props).Result as OkObjectResult;

//             // Assert
//             var items = Assert.IsType<List<DiskVM>>(okResult.Value);
//             Assert.Equal(1, items.Count);
//         }


//         // [Fact]
//         // public void GetFiltered_WhenCalled_ReturnsAllItems()
//         // {
//         //     var pack = new DbCallProps();
//         //     pack.property = "Price";
//         //     pack.value = 2000;
//         //     pack.sign = Operators.GreaterOrEqual;

//         //     var pack2 = new DbCallProps();
//         //     pack2.property = "Name";
//         //     pack2.value = "Di69";
//         //     pack2.sign = Operators.Contains;
//         //     var data = new List<DbCallProps>();
//         //     data.Add(pack);
//         //     data.Add(pack2);

//         //     // Act
//         //     var okResult = _service.GetFilteredDisks(data);
//         //     // Assert
//         //     Assert.Equal(2,okResult.Count);
//         // }
//         //  [Fact]
//         // public void GetPaged_WhenCalled_ReturnsAllItems()
//         // {

//         //     // Act
//         //     var okResult = _service.GetPagedDisks(1,3);
//         //     // Assert
//         //     Assert.Equal(3,okResult.Count);
//         // }
//         //  [Fact]
//         // public void GetSorted_WhenCalled_ReturnsAllItems()
//         // {

//         //     // Act
//         //     var okResult = _service.GetSortedDisks("Price");
//         //     // Assert
//         //     Assert.Equal(5,okResult.Count);
//         // }

        
//         // GetDsikById Tests
//         [Fact]
//         public void GetDiskById_UnknownIdPassed_ReturnsNotFoundResult()
//         {
//             // Act
//             var notFoundResult = _controller.GetDiskById(78);

//             // Assert
//             Assert.ThrowsAsync<DataNotFoundException>(() => _controller.GetDiskById(78));
//         }

//         [Fact]
//         public void GetDiskById_ExistingIdPassed_ReturnsOkResult()
//         {
//             // Arrange
//             int testId = 1;

//             // Act
//             var okResult = _controller.GetDiskById(testId);

//             // Assert
//             Assert.IsType<OkObjectResult>(okResult.Result);
//         }

//         [Fact]
//         public void GetDiskById_ExistingIdPassed_ReturnsRightItem()
//         {
//             // Arrange
//             int testId = 1;

//             // Act
//             var okResult = _controller.GetDiskById(testId).Result as OkObjectResult;

//             // Assert
//             Assert.IsType<DiskVM>(okResult.Value);
//             Assert.Equal(testId, (okResult.Value as DiskVM).Id);
//         }

//         // AddDisk Tests
//         [Fact]
//         public void AddDisk_InvalidObjectPassed_ReturnsBadRequest()
//         {
//             // Arrange
//             var priceMissingItem = new DiskEditVM()
//             {
//                 Name = "Taz"
//             };
//             _controller.ModelState.AddModelError("Price", "Required");

//             // Act
//             var badResponse = _controller.AddDisk(priceMissingItem);

//             // Assert
//             Assert.IsType<BadRequestObjectResult>(badResponse);
//         }


//         [Fact]
//         public void AddDisk_ValidObjectPassed_ReturnsCreatedResponse()
//         {
//             // Arrange
//             DiskEditVM testItem = new DiskEditVM()
//             {
//                 Name = "GDisk",
//                 Price = 1223,
//                 Url = null,
//             };

//             // Act
//             var createdResponse = _controller.AddDisk(testItem);

//             // Assert
//             Assert.IsType<CreatedAtActionResult>(createdResponse);
//         }


//         [Fact]
//         public void AddDisk_ValidObjectPassed_ReturnedResponseHasCreatedItem()
//         {
//             // Arrange
//             DiskEditVM testItem = new DiskEditVM()
//             {
//                 Name = "GDisk",
//                 Price = 1223,
//                 Url = null,
//             };

//             // Act
//             var createdResponse = _controller.AddDisk(testItem) as CreatedAtActionResult;
//             var item = createdResponse.Value as DiskEditVM;

//             // Assert
//             Assert.IsType<DiskEditVM>(item);
//             Assert.Equal("GDisk", item.Name);
//         }
//         // DeleteDisk Tests

//         [Fact]
//         public void DeleteDisk_NotExistingIdPassed_ReturnsNotFoundResponse()
//         {
//             // Arrange
//             int notExistingGuid = 67;

//             // Assert
//             Assert.Throws<NullReferenceException>(() => _controller.DeleteDisk(notExistingGuid));
//         }

//         [Fact]
//         public void DeleteDisk_ExistingIdPassed_ReturnsOkResult()
//         {
//             // Arrange
//             int existingGuid = 5;

//             // Act
//             var okResponse = _controller.DeleteDisk(existingGuid);

//             // Assert
//             Assert.IsType<OkResult>(okResponse);
//         }
//         [Fact]
//         public void DeleteDisk_ExistingIdPassed_RemovesOneItem()
//         {
//             // Arrange
//             var existingGuid = 4;

//             // Act
//             var okResponse = _controller.DeleteDisk(existingGuid);

//             // Assert
//             Assert.Equal(4, _service.GetAllDisks(null).Count);
//         }

//         // Bugs
//         [Fact]
//         public void UpdateDisk_ValidObjectPassed_UpadtesWithDiskImage()
//         {
//             // Arrange
//             var a = _service.GetDisk(5);
//             var testItem = _mapper.Map<DiskEditVM>(a);
//             testItem.Name = "GDisk-Sm76";
//             testItem.Company = "TagAz";
//             testItem.Price = 1567;
//             testItem.Url = null;
//             testItem.ImageName = "Setter";
//             _controller.UpdateDisk(testItem);
//             _unitOfWork.Save();
//             // Act
//             var updateResponce = _service.GetDisk(testItem.Id);

//             // Assert
//             Assert.Equal(testItem.Id, updateResponce.Id);
//             Assert.Equal(testItem.Name, updateResponce.Name);
//             Assert.Equal(testItem.Company, updateResponce.Company);
//             Assert.Equal(testItem.Price, updateResponce.Price);
//             Assert.Equal(testItem.ImageName, updateResponce.ImageName);
//             Assert.Equal(testItem.Url, updateResponce.Url);
//         }
//         [Fact]
//         public void UpdateDisk_ValidObjectPassed_UpadtesWithoutDiskImage()
//         {
//             // Arrange
//             var a = _service.GetDisk(5);
//             var testItem = _mapper.Map<DiskEditVM>(a);
//             testItem.Name = "GDisk-Sm76";
//             testItem.Company = "TagAz";
//             testItem.Price = 1567;
//             _controller.UpdateDisk(testItem);
//             _unitOfWork.Save();
//             // Act
//             var updateResponce = _service.GetDisk(testItem.Id);

//             // Assert
//             Assert.Equal(testItem.Id, updateResponce.Id);
//             Assert.Equal(testItem.Name, updateResponce.Name);
//             Assert.Equal(testItem.Company, updateResponce.Company);
//             Assert.Equal(testItem.Price, updateResponce.Price);
//             Assert.Equal(testItem.ImageName, "Di693img");
//             Assert.Equal(testItem.Url, "somepath5");
//         }

//         [Fact]
//         public void UpdateDisk_ValidObjectPassed_UpadtesOnlyDiskImage()
//         {
//             // Arrange
//             var a = _service.GetDisk(5);
//             var testItem = _mapper.Map<DiskEditVM>(a);
//             var name = testItem.Name;
//             var company = testItem.Company;
//             var price = testItem.Price;
//             testItem.Url = null;
//             testItem.ImageName = "Setter";
//             _controller.UpdateDisk(testItem);
//             _unitOfWork.Save();
//             // Act
//             var updateResponce = _service.GetDisk(testItem.Id);

//             // Assert
//             Assert.Equal(testItem.Id, updateResponce.Id);
//             Assert.Equal(testItem.Name, name);
//             Assert.Equal(testItem.Company, company);
//             Assert.Equal(testItem.Price, price);
//             Assert.Equal(testItem.ImageName, updateResponce.ImageName);
//             Assert.Equal(testItem.Url, updateResponce.Url);
//         }
//     }
// }
