using ADO.NET;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DemoWPF_DAO.DAO
{
    public class StudentDAO
    {
        public static List<Student> GetAllStudents()
        {
            string sql = "SELECT * FROM Student";
            DataTable dt = DAO.GetDataBySQl(sql);

            List<Student> students = new List<Student>();

            foreach (DataRow row in dt.Rows)
            {
                students.Add(new Student
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    Gender = Convert.ToBoolean(row["gender"]),
                    DepartId = row["departId"].ToString(),
                    Dob = row["dob"] != DBNull.Value ? Convert.ToDateTime(row["dob"]) : (DateTime?)null,
                    Gpa = Convert.ToDouble(row["gpa"]),
                });
            }

            return students;
        }


        public static int Add(Student student)
        {
            string sql = "INSERT INTO Student (Name, Gender, DepartId, Dob, Gpa) " +
                         "VALUES (@Name, @Gender, @DepartId, @Dob, @Gpa)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@Gender", student.Gender),
                new SqlParameter("@DepartId", student.DepartId),
                new SqlParameter("@Dob", (object)student.Dob ?? DBNull.Value),
                new SqlParameter("@Gpa", student.Gpa),
            };

            return DAO.ExecuteSql(sql, parameters);
        }

        public static int Update(Student student)
        {
            string sql = "UPDATE Student SET " +
                            "Name = @Name, " +
                            "Gender = @Gender, " +
                            "DepartId = @DepartId, " +
                            "Dob = @Dob, " +
                            "Gpa = @Gpa" +
                            "WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", student.Id),
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@Gender", student.Gender),
                new SqlParameter("@DepartId", student.DepartId),
                new SqlParameter("@Dob", (object)student.Dob ?? DBNull.Value),
                new SqlParameter("@Gpa", student.Gpa),
            };

            return DAO.ExecuteSql(sql, parameters);
        }

        public static int Remove(int studentId)
        {
            string sql = "DELETE FROM Student WHERE Id = @Id";
            SqlParameter parameter = new SqlParameter("@Id", studentId);

            return DAO.ExecuteSql(sql, parameter);
        }
    }
}
