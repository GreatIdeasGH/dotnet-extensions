﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace GreatIdeas.PagedList.Tests;

public class PagedListFacts
{
    //[Fact]
    //public void Null_Data_Set_Doesnt_Throw_Exception()
    //{
    //	//act
    //	Assert.ThrowsDelegate act = () => new PagedList<object>(null, 1, 10);

    //	//assert
    //	Assert.DoesNotThrow(act);
    //}

    //[Fact]
    //public void PageNumber_Below_One_Throws_ArgumentOutOfRange()
    //{
    //    //arrange
    //    var data = new[] { 1, 2, 3 };

    //    //act
    //    Action action = () => data.ToPagedList(0, 1);

    //    //assert
    //    Assert.Throws<ArgumentOutOfRangeException>(action);
    //}

    [Fact]
    public void PageNumber_Below_One_Returns_One()
    {
        //arrange
        var data = new[] { 1, 2, 3 };

        //act
        var pagedList = data.ToPagedList(0, 1);

        //assert
        Assert.Equal(1, pagedList.PageNumber);
    }

    [Fact]
    public async Task Argument_out_of_range()
    {
        var queryable = new List<object>().AsQueryable();
        var list = queryable.ToList();
        var pagedList = list.ToPagedList();

        Assert.NotNull(pagedList);
    }


    [Fact]
    public void Split_Works()
    {
        //arrange
        var list = Enumerable.Range(1, 47);

        //act
        var splitList = list.Split(5).ToList();

        //assert
        Assert.Equal(5, splitList.Count());
        Assert.Equal(10, splitList.ElementAt(0).Count());
        Assert.Equal(10, splitList.ElementAt(1).Count());
        Assert.Equal(10, splitList.ElementAt(2).Count());
        Assert.Equal(10, splitList.ElementAt(3).Count());
        Assert.Equal(7, splitList.ElementAt(4).Count());
    }

    [Fact]
    public void Key_Selector_Works()
    {
        var collection = Enumerable.Range(1, 1000000);

        var pageNumber = 2;
        var pageSize = 10;

        Expression<Func<int, int>> keySelector = i => Order(i);

        var list = collection.ToPagedList(keySelector, pageNumber, pageSize);

        Assert.Equal(22, list.ElementAt(0));
        Assert.Equal(24, list.ElementAt(1));
        Assert.Equal(26, list.ElementAt(2));
        Assert.Equal(28, list.ElementAt(3));
        Assert.Equal(30, list.ElementAt(4));
        Assert.Equal(32, list.ElementAt(5));
        Assert.Equal(34, list.ElementAt(6));
        Assert.Equal(36, list.ElementAt(7));
        Assert.Equal(38, list.ElementAt(8));
        Assert.Equal(40, list.ElementAt(9));
    }

    private static int Order(int i)
    {
        //
        return i % 2 == 0 ? 1 : 10;
    }

    [Fact]
    public void PageNumber_Above_RecordCount_Returns_Empty_List()
    {
        //arrange
        var data = new[] { 1, 2, 3 };

        //act
        var pagedList = data.ToPagedList(2, 3);

        //assert
        Assert.Equal(0, pagedList.Count);
    }

    // [Fact]
    // public void PageSize_Below_One_Throws_ArgumentOutOfRange()
    // {
    //     //arrange
    //     var data = new[] { 1, 2, 3 };

    //     //act
    //     Action action = () => data.ToPagedList(1, 0);

    //     //assert
    //     Assert.Throws<ArgumentOutOfRangeException>(action);
    // }
    [Fact]
    public void PageSize_Below_One_Returns_DefaultSize()
    {
        //arrange
        var data = new[] { 1, 2, 3 };

        //act
        var pagedList = data.ToPagedList(-1, 100);

        //assert
        Assert.Equal(1, pagedList.PageNumber);
    }

    [Fact]
    public void Null_Data_Set_Doesnt_Return_Null()
    {
        //act
        var pagedList = new PagedList<object>(null, 1, 10);

        //assert
        Assert.NotNull(pagedList);
    }

    [Fact]
    public void Null_Data_Set_Returns_Zero_Pages()
    {
        //act
        var pagedList = new PagedList<object>(null, 1, 10);

        //assert
        Assert.Equal(0, pagedList.PageCount);
    }

    [Fact]
    public void Zero_Item_Data_Set_Returns_Zero_Pages()
    {
        //arrange
        var data = new List<object>();

        //act
        var pagedList = data.ToPagedList(1, 10);

        //assert
        Assert.Equal(0, pagedList.PageCount);
    }

    [Fact]
    public void DataSet_Of_One_Through_Five_PageSize_Of_Two_PageNumber_Of_Two_First_Item_Is_Three()
    {
        //arrange
        var data = new[] { 1, 2, 3, 4, 5 };

        //act
        var pagedList = data.ToPagedList(2, 2);

        //assert
        Assert.Equal(3, pagedList[0]);
    }

    [Fact]
    public void TotalCount_Is_Preserved()
    {
        //arrange
        var data = new[] { 1, 2, 3, 4, 5 };

        //act
        var pagedList = data.ToPagedList(2, 2);

        //assert
        Assert.Equal(5, pagedList.TotalItemCount);
    }

    [Fact]
    public void PageSize_Is_Preserved()
    {
        //arrange
        var data = new[] { 1, 2, 3, 4, 5 };

        //act
        var pagedList = data.ToPagedList(2, 2);

        //assert
        Assert.Equal(2, pagedList.PageSize);
    }

    [Fact]
    public void Data_Is_Filtered_By_PageSize()
    {
        //arrange
        var data = new[] { 1, 2, 3, 4, 5 };

        //act
        var pagedList = data.ToPagedList(2, 2);

        //assert
        Assert.Equal(2, pagedList.Count);

        //### related test below

        //act
        pagedList = data.ToPagedList(3, 2);

        //assert
        Assert.Equal(1, pagedList.Count);
    }

    [Fact]
    public void DataSet_OneThroughSix_PageSize_Three_PageNumber_One_FirstValue_Is_One()
    {
        //arrange
        var data = new[] { 1, 2, 3, 4, 5, 6 };

        //act
        var pagedList = data.ToPagedList(1, 3);

        //assert
        Assert.Equal(1, pagedList[0]);
    }

    [Fact]
    public void DataSet_OneThroughThree_PageSize_One_PageNumber_Three_HasNextPage_False()
    {
        //arrange
        var data = new[] { 1, 2, 3 };

        //act
        var pagedList1 = data.ToPagedList(2, 1);
        var pagedList2 = data.ToPagedList(3, 1);
        var pagedList3 = data.ToPagedList(4, 1);

        //assert
        Assert.True(pagedList1.HasNextPage);
        Assert.False(pagedList2.HasNextPage);
        Assert.False(pagedList3.HasNextPage);
    }

    [Fact]
    public void DataSet_OneThroughThree_PageSize_One_PageNumber_Three_IsLastPage_True()
    {
        //arrange
        var data = new[] { 1, 2, 3 };

        //act
        var pagedList1 = data.ToPagedList(2, 1);
        var pagedList2 = data.ToPagedList(3, 1);
        var pagedList3 = data.ToPagedList(4, 1);

        //assert
        Assert.False(pagedList1.IsLastPage);
        Assert.True(pagedList2.IsLastPage);
        Assert.False(pagedList3.IsLastPage);
    }

    [Fact]
    public void DataSet_OneAndTwo_PageSize_One_PageNumber_Two_FirstValue_Is_Two()
    {
        //arrange
        var data = new[] { 1, 2 };

        //act
        var pagedList = data.ToPagedList(2, 1);

        //assert
        Assert.Equal(2, pagedList[0]);
    }

    [Fact]
    public void DataSet_OneThroughTen_PageSize_Five_PageNumber_One_FirstItemOnPage_Is_One()
    {
        //arrange
        var data = Enumerable.Range(1, 10);

        //act
        var pagedList = data.ToPagedList(1, 5);

        //assert
        Assert.Equal(1, pagedList.FirstItemOnPage);
    }

    [Fact]
    public void DataSet_OneThroughTen_PageSize_Five_PageNumber_Two_FirstItemOnPage_Is_Six()
    {
        //arrange
        var data = Enumerable.Range(1, 10);

        //act
        var pagedList = data.ToPagedList(2, 5);

        //assert
        Assert.Equal(6, pagedList.FirstItemOnPage);
    }

    [Fact]
    public void DataSet_OneThroughTen_PageSize_Five_PageNumber_One_LastItemOnPage_Is_Five()
    {
        //arrange
        var data = Enumerable.Range(1, 10);

        //act
        var pagedList = data.ToPagedList(1, 5);

        //assert
        Assert.Equal(5, pagedList.LastItemOnPage);
    }

    [Fact]
    public void DataSet_OneThroughTen_PageSize_Five_PageNumber_Two_LastItemOnPage_Is_Ten()
    {
        //arrange
        var data = Enumerable.Range(1, 10);

        //act
        var pagedList = data.ToPagedList(2, 5);

        //assert
        Assert.Equal(10, pagedList.LastItemOnPage);
    }

    [Fact]
    public void DataSet_OneThroughEight_PageSize_Five_PageNumber_Two_LastItemOnPage_Is_Eight()
    {
        //arrange
        var data = Enumerable.Range(1, 8);

        //act
        var pagedList = data.ToPagedList(2, 5);

        //assert
        Assert.Equal(8, pagedList.LastItemOnPage);
    }


    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 1, 1, false, true)]
    [InlineData(new[] { 1, 2, 3 }, 2, 1, true, true)]
    [InlineData(new[] { 1, 2, 3 }, 3, 1, true, false)]
    [InlineData(new[] { 1, 2, 3 }, 1, 3, false, false)]
    [InlineData(new[] { 1, 2, 3 }, 2, 3, false, false)]
    [InlineData(new int[] { }, 1, 3, false, false)]
    public void Theory_HasPreviousPage_And_HasNextPage_Are_Correct(int[] integers, int pageNumber, int pageSize,
        bool expectedHasPrevious, bool expectedHasNext)
    {
        //arrange
        var data = integers;

        //act
        var pagedList = data.ToPagedList(pageNumber, pageSize);

        //assert
        Assert.Equal(expectedHasPrevious, pagedList.HasPreviousPage);
        Assert.Equal(expectedHasNext, pagedList.HasNextPage);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 1, 1, true, false)]
    [InlineData(new[] { 1, 2, 3 }, 2, 1, false, false)]
    [InlineData(new[] { 1, 2, 3 }, 3, 1, false, true)]
    [InlineData(new[] { 1, 2, 3 }, 1, 3, true, true)] // Page 1 of 1
    [InlineData(new[] { 1, 2, 3 }, 2, 3, false, false)] // Page 2 of 1
    [InlineData(new int[] { }, 1, 3, false, false)] // Page 1 of 0
    public void Theory_IsFirstPage_And_IsLastPage_Are_Correct(int[] integers, int pageNumber, int pageSize,
        bool expectedIsFirstPage, bool expectedIsLastPage)
    {
        //arrange
        var data = integers;

        //act
        var pagedList = data.ToPagedList(pageNumber, pageSize);

        //assert
        Assert.Equal(expectedIsFirstPage, pagedList.IsFirstPage);
        Assert.Equal(expectedIsLastPage, pagedList.IsLastPage);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, 1, 3)]
    [InlineData(new[] { 1, 2, 3 }, 3, 1)]
    [InlineData(new[] { 1 }, 1, 1)]
    [InlineData(new[] { 1, 2, 3 }, 2, 2)]
    [InlineData(new[] { 1, 2, 3, 4 }, 2, 2)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 3)]
    [InlineData(new int[] { }, 1, 0)]
    public void Theory_PageCount_Is_Correct(int[] integers, int pageSize, int expectedNumberOfPages)
    {
        //arrange
        var data = integers;

        //act
        var pagedList = data.ToPagedList(1, pageSize);

        //assert
        Assert.Equal(expectedNumberOfPages, pagedList.PageCount);
    }

    [Fact]
    public void PageCount_Is_Correct_Big()
    {
        //arrange
        var data = Enumerable.Range(1, 100001).ToArray();

        //act
        var pagedList = data.ToPagedList(1, 100000);

        //assert
        Assert.Equal(2, pagedList.PageCount);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 1, 2, 1, 2)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 2, 3, 4)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 3, 2, 5, 5)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 4, 2, 0, 0)]
    [InlineData(new int[] { }, 1, 2, 0, 0)]
    public void Theory_FirstItemOnPage_And_LastItemOnPage_Are_Correct(int[] integers, int pageNumber, int pageSize, int expectedFirstItemOnPage, int expectedLastItemOnPage)
    {
        //arrange
        var data = integers;

        //act
        var pagedList = data.ToPagedList(pageNumber, pageSize);

        //assert
        Assert.Equal(expectedFirstItemOnPage, pagedList.FirstItemOnPage);
        Assert.Equal(expectedLastItemOnPage, pagedList.LastItemOnPage);
    }
}