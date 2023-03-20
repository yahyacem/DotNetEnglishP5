using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using DotNetEnglishP5.Controllers;
using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models;
using DotNetEnglishP5.Models.Repositories;
using DotNetEnglishP5.Models.Services;
using DotNetEnglishP5.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using X.PagedList;

namespace DotNetEnglishP5.Tests.Unit
{
    [Collection("Sequential")]
    public class CarServiceUnitTests
    {
        private DbContextOptions<ApplicationDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ProductServiceRead" + Guid.NewGuid().ToString())
            .Options;
        }
    }
}
