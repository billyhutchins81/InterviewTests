using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        private GraduationTracker _tracker;
        private Diploma _diploma;
        private Student[] _students;

        [TestInitialize]
        public void Setup()
        {
            _tracker = new GraduationTracker();
            _diploma = new Diploma
            {
                Id = 1,
                Credits = 4,
                Requirements = new[] {100, 102, 103, 104}
            };
            const int magnaCumLaudeStudentGrades = 96;
            const int sumaCumLaudeStudentGrades = 80;
            const int averageStudentGrades = 50;
            const int remedialStudentGrades = 40;
            _students = new[]
            {
                new Student
                {
                    Id = 1,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = magnaCumLaudeStudentGrades},
                        new Course {Id = 2, Name = "Science", Mark = magnaCumLaudeStudentGrades},
                        new Course {Id = 3, Name = "Literature", Mark = magnaCumLaudeStudentGrades},
                        new Course {Id = 4, Name = "Physical Education", Mark = magnaCumLaudeStudentGrades}
                    }
                },
                new Student
                {
                    Id = 2,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = sumaCumLaudeStudentGrades},
                        new Course {Id = 2, Name = "Science", Mark = sumaCumLaudeStudentGrades},
                        new Course {Id = 3, Name = "Literature", Mark = sumaCumLaudeStudentGrades},
                        new Course {Id = 4, Name = "Physical Education", Mark = sumaCumLaudeStudentGrades}
                    }
                },
                new Student
                {
                    Id = 3,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = averageStudentGrades},
                        new Course {Id = 2, Name = "Science", Mark = averageStudentGrades},
                        new Course {Id = 3, Name = "Literature", Mark = averageStudentGrades},
                        new Course {Id = 4, Name = "Physical Education", Mark = averageStudentGrades}
                    }
                },
                new Student
                {
                    Id = 4,
                    Courses = new[]
                    {
                        new Course {Id = 1, Name = "Math", Mark = remedialStudentGrades},
                        new Course {Id = 2, Name = "Science", Mark = remedialStudentGrades},
                        new Course {Id = 3, Name = "Literature", Mark = remedialStudentGrades},
                        new Course {Id = 4, Name = "Physical Education", Mark = remedialStudentGrades}
                    }
                }
            };
        }

        [TestMethod]
        public void TestHasDiploma()
        {
            var graduated = (from student in _students
                let hasDiploma = true
                where _tracker.HasGraduated(_diploma, student).Item1.Equals(hasDiploma)
                select _tracker.HasGraduated(_diploma, student)).ToList();
            Assert.AreEqual(3, graduated.Count, "The number of graduates are not equal to 3");
        }

        [TestMethod]
        public void TestHasRemedial()
        {
            var remedialStudents = (from student in _students
                where _tracker.HasGraduated(_diploma, student).Item2.Equals(STANDING.Remedial)
                select _tracker.HasGraduated(_diploma, student)).ToList();
            Assert.AreEqual(1, remedialStudents.Count,
                "The number of students with Remedial standing are not equal to 1");
        }

        [TestMethod]
        public void TestHasAverage()
        {
            var averageStudents = (from student in _students
                where _tracker.HasGraduated(_diploma, student).Item2.Equals(STANDING.Average)
                select _tracker.HasGraduated(_diploma, student)).ToList();
            Assert.AreEqual(1, averageStudents.Count, "The number of students with Average standing is not equal to 1");
        }

        [TestMethod]
        public void TestHasSumaCumLaude()
        {
            var sumaCumLaudeStudents = (from student in _students
                where _tracker.HasGraduated(_diploma, student).Item2.Equals(STANDING.SumaCumLaude)
                select _tracker.HasGraduated(_diploma, student)).ToList();
            Assert.AreEqual(1, sumaCumLaudeStudents.Count,
                "The number of students with SumaCumLaude standing is not equal to 1");
        }

        [TestMethod]
        public void TestHasMagnaCumLaude()
        {
            var magnaCumLaudeStudents = (from student in _students
                where _tracker.HasGraduated(_diploma, student).Item2.Equals(STANDING.MagnaCumLaude)
                select _tracker.HasGraduated(_diploma, student)).ToList();
            Assert.AreEqual(1, magnaCumLaudeStudents.Count,
                "The number of students with MagnaCumLaude standing is not equal to 1");
        }
    }
}