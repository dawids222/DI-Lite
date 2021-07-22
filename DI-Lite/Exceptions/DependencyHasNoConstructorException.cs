﻿using System;

namespace DI_Lite.Exceptions
{
    public class DependencyHasNoConstructorException : Exception
    {
        public Type Type { get; }

        public DependencyHasNoConstructorException(Type type)
            : base($"Dependency of type {type.FullName} can not be registered, because it contains no public constructor. Dependecy types must contain only 1 public constructor")
        {
            Type = type;
        }
    }
}