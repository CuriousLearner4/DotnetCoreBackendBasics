using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Model;
using SchoolManagementSystem.Repository.Interface;

namespace SchoolManagementSystem.Controller
{
    [Route("api")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepository StudentRepository { get; init; }
        public StudentController(IStudentRepository studentRepository)
        {
            StudentRepository = studentRepository;
        }

        [HttpGet("/getstring")]
        public async Task<ActionResult<string>> GetString()
        {
            return Ok(Environment.GetEnvironmentVariable("SQL_PL_CS"));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>>  GetAll()
        {
            var students = await StudentRepository.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}",Name ="GetStudent")]
        public async Task<ActionResult<Student>>  GetStudentById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var student = await StudentRepository.GetStudent(id);
            if(student == null){
                ModelState.AddModelError("","Student with given id not found");
                return NotFound(ModelState);
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDTO newStudent)
        {
            if (newStudent.Name.IsNullOrEmpty())
            {
                return BadRequest("Invalid input");
            }
            Student student = new() { Name = newStudent.Name };
            await StudentRepository.CreateStudent(student);
            return CreatedAtRoute("GetStudent", new {id=student.Id},student);
        }

    }
}
