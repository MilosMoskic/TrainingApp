﻿using TrainingApp.Domain.Models;

namespace TrainingApp.Domain.Interfaces
{
    public interface IWodRepository
    {
        Wod CreateWod(Wod wod);
    }
}