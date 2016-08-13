using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PhantomNet.Searching.Mvc
{
    public static class FilterExtensions
    {
        public static IEnumerable<SelectListItem> GenerateSelectList(
            this Filter<IntegerFilterOption> filter)
        {
            return filter.Options.Select(
                x => new SelectListItem {
                    Value = x.Value.ToString(),
                    Text = x.DisplayText,
                    Disabled = !x.IsAvailable,
                    Selected = x.IsActive
                });
        }

        public static IEnumerable<SelectListItem> GenerateSelectList(
            this Filter<CurrencyFilterOption> filter)
        {
            return filter.Options.Select(
                x => new SelectListItem {
                    Value = x.Value.ToString(),
                    Text = x.DisplayText,
                    Disabled = !x.IsAvailable,
                    Selected = x.IsActive
                });
        }

        public static IEnumerable<SelectListItem> GenerateSelectList<TKey>(
            this Filter<KeyValueFilterOption<TKey>> filter)
            where TKey : IEquatable<TKey>
        {
            return GenerateSelectList(filter, false);
        }

        public static IEnumerable<SelectListItem> GenerateSelectList<TKey>(
            this Filter<KeyValueFilterOption<TKey>> filter,
            IEnumerable<SelectListGroup> groups)
            where TKey : IEquatable<TKey>
        {
            return GenerateSelectList(filter, false, groups);
        }

        public static IEnumerable<SelectListItem> GenerateSelectList<TKey>(
            this Filter<KeyValueFilterOption<TKey>> filter,
            bool InactiveOnly)
            where TKey : IEquatable<TKey>
        {
            return GenerateSelectList(filter, InactiveOnly, new SelectListGroup[0]);
        }

        public static IEnumerable<SelectListItem> GenerateSelectList<TKey>(
            this Filter<KeyValueFilterOption<TKey>> filter,
            bool InactiveOnly,
            IEnumerable<SelectListGroup> groups)
            where TKey : IEquatable<TKey>
        {
            if (groups == null)
            {
                throw new ArgumentNullException(nameof(groups));
            }

            return filter.Options.Where(x => InactiveOnly ? !x.IsActive : true)
                                 .Select(
                x => new SelectListItem {
                    Value = x.Key.ToString(),
                    Text = x.DisplayText,
                    Disabled = !x.IsAvailable,
                    Selected = x.IsActive,
                    Group = groups.SingleOrDefault(y => y.Name == x.Group)
                });
        }
    }
}
