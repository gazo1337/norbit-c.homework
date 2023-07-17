﻿namespace TaskDomain
{
    public class Task
    {
        public long Id { get; set; }
        public string taskName { get; set; } = "";
        public string devName { get; set; } = "";
        public string taskDescript { get; set; } = "";
        public int compl { get; set; } = 0;
        public int level { get; set; } = 0;
        public int price { get; set; } = 0;
        public bool complete { get; set; } = false;
    }
}