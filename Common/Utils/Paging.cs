using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.RequestHandlers;
using Common.Requests;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Common.Utils
{
    public class Paging<T>
    {
        public Paging(IEnumerable<T> items, int count, int index, int size)
        {
            Index = index;
            Total = (int) Math.Ceiling(count / (double) size);
            Items = items;
        }

        public IEnumerable<T> Items { get; }
        public int Index { get; }
        public int Total { get; }

        public bool HasPrevious => Index > 1;
        public bool HasNext => Index < Total;

        public static async Task<Paging<T>> CreateAsync(IQueryable<T> source, int index, int size)
        {
            Validate(index, size);

            var count = await source.CountAsync();
            var items = await source.Skip((index - 1) * size).Take(size).ToListAsync();

            return new Paging<T>(items, count, index, size);
        }

        public static Task<Paging<T>> CreateAsync(IQueryable<T> source, IPageableRequest<T> request)
        {
            return CreateAsync(source, request.PageIndex, request.PageSize);
        }

        public static Paging<T> EmptyPage()
        {
            return new(new List<T>(), 0, 1, 1);
        }

        public static void Validate(int index, int size)
        {
            var failures = new List<ValidationFailure>();

            if (index < 1)
                failures.Add(new ValidationFailure("Index", $"must be at least 1. You entered {index}."));
            if (size < 1)
                failures.Add(new ValidationFailure("Size", $"must be at least 1. You entered {size}."));

            if (failures.Count > 0) throw new ValidationException(failures);
        }
    }
}