using Infrastructure.Dtos;
using Infrastructure.Models.Components;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class CourseDetailsViewModel
    {
        public CourseDto Course { get; set; } = new CourseDto();

        public ProgramCourseBoxModel ProgramBox = new()
        {
            ProgramBoxes = [
                new(){Title ="Introduction. Getting started", Text ="Nulla faucibus mauris pellentesque blandit faucibus non. Sit ut et at suspendisse gravida hendrerit tempus placerat." },
                new(){Title ="The ultimate HTML developer: advanced HTML", Text ="Lobortis diam elit id nibh ultrices sed penatibus donec. Nibh iaculis eu sit cras ultricies. Nam eu eget etiam egestas donec scelerisque ut ac enim. Vitae ac nisl, enim nec accumsan vitae est." },
                new(){Title ="CSS & CSS3: basic", Text ="Duis euismod enim, facilisis risus tellus pharetra lectus diam neque. Nec ultrices mi faucibus est. Magna ullamcorper potenti elementum ultricies auctor nec volutpat augue." },
                new(){Title ="JavaScript basics for beginners", Text ="Morbi porttitor risus imperdiet a, nisl mattis. Amet, faucibus eget in platea vitae, velit, erat eget velit. At lacus ut proin erat." },
                new(){Title ="Understanding APIs", Text ="Risus morbi euismod in congue scelerisque fusce pellentesque diam consequat. Nisi mauris nibh sed est morbi amet arcu urna. Malesuada feugiat quisque consectetur elementum diam vitae. Dictumst facilisis odio eu quis maecenas risus odio fames bibendum." },
                new(){Title ="C# and .NET from beginner to advanced", Text ="Quis risus quisque diam diam. Volutpat neque eget eu faucibus sed urna fermentum risus. Est, mauris morbi nibh massa." }
                ]
        };
    }
}
