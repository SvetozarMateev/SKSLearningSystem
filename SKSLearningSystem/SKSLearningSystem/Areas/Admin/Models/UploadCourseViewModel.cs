﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKSLearningSystem.Areas.Admin.Models
{
    public class UploadCourseViewModel
    {
        public IEnumerable<HttpPostedFileBase> Photos { get; set; }

        public HttpPostedFileBase CourseFile { get; set; }

    }
}