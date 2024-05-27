﻿using ProjectApi.Entities;
using System.ComponentModel;

namespace MyApplication.ViewModels.Orders
{
	public class EditVM
	{
        public Order Order { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Worker> Workers { get; set; } = new List<Worker>();
    }
}
