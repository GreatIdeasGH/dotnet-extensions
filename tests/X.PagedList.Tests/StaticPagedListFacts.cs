﻿using System;
using Xunit;

namespace GreatIdeas.PagedList.Tests;

public class StaticPagedListFacts
{
    [Theory,
     InlineData(1, true, false),
     InlineData(2, false, false),
     InlineData(3, false, true),
     InlineData(4, false, false)]
    public void StaticPagedList_uses_supplied_totalItemCount_to_determine_subsets_position_within_superset(int pageNumber, bool shouldBeFirstPage, bool shouldBeLastPage)
    {
        //arrange
        var subset = new[] { 1, 1, 1 };

        //act
        var list = new StaticPagedList<int>(subset, pageNumber, 3, 9);

        //assert
        Assert.Equal(pageNumber, list.PageNumber);
        Assert.Equal(shouldBeFirstPage, list.IsFirstPage);
        Assert.Equal(shouldBeLastPage, list.IsLastPage);
    }

    [Fact]
    public void TotalItemCount_Below_One_Throws_Exception()
    {
        //arrange
        var data = new[] { 1, 2, 3, 4, 5 };

        //assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new StaticPagedList<int>(data, 1, 10, -1));
    }
}