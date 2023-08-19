using Autodesk.Revit.DB;
using Craftify.Geometry.Builders;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Shared;
using Xunit;

namespace Craftify.Geometry.Tests
{
    public class BoundingBoxXYZExtensionsTests
    {
        [Fact]
        public void Align_ShouldSetCorrectValue_WhenLengthAlignmentIsLeft()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Length, Alignment.Left);
            Assert.True(box.Min.X.IsAlmostEqualTo(0) && box.Max.X.IsAlmostEqualTo(3));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenLengthAlignmentIsCenter()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Length, Alignment.Center);
            Assert.True(box.Min.X.IsAlmostEqualTo(-1.5) && box.Max.X.IsAlmostEqualTo(1.5));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenLengthAlignmentIsRight()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Length, Alignment.Right);
            Assert.True(box.Min.X.IsAlmostEqualTo(-3) && box.Max.X.IsAlmostEqualTo(0));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenWidthAlignmentIsLeft()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Width, Alignment.Left);
            Assert.True(box.Min.Y.IsAlmostEqualTo(0) && box.Max.Y.IsAlmostEqualTo(5));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenWidthAlignmentIsCenter()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Width, Alignment.Center);
            Assert.True(box.Min.Y.IsAlmostEqualTo(-2.5) && box.Max.Y.IsAlmostEqualTo(2.5));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenWidthAlignmentIsRight()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Width, Alignment.Right);
            Assert.True(box.Min.Y.IsAlmostEqualTo(-5) && box.Max.Y.IsAlmostEqualTo(0));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenHeightAlignmentIsLeft()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Height, Alignment.Left);
            Assert.True(box.Min.Z.IsAlmostEqualTo(0) && box.Max.Z.IsAlmostEqualTo(4));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenHeightAlignmentIsCenter()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Height, Alignment.Center);
            Assert.True(box.Min.Z.IsAlmostEqualTo(-2) && box.Max.Z.IsAlmostEqualTo(2));
        }

        [Fact]
        public void Align_ShouldSetCorrectValue_WhenHeightAlignmentIsRight()
        {
            var box = CreateBoundingBox();
            box.Align(Side.Height, Alignment.Right);
            Assert.True(box.Min.Z.IsAlmostEqualTo(-4) && box.Max.Z.IsAlmostEqualTo(0));
        }

        private BoundingBoxXYZ CreateBoundingBox()
        {
            return new BoundingBoxBuilder()
                .OfLength(3)
                .OfWidth(5)
                .OfHeight(4)
                .WithTransform(Transform.CreateTranslation(new XYZ(2, 5, 0)))
                .Build();
        }
    }
}