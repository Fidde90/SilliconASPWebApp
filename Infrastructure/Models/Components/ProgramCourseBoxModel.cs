
namespace Infrastructure.Models.Components
{
    public class ProgramCourseBoxModel
    {
        public int Number {  get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public List<ProgramCourseBoxModel> ProgramBoxes = [];
    }
}
