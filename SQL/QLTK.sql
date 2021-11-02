USE [QLTK]
GO

/****** Object:  Table [dbo].[NGUOIDUNG]    Script Date: 02/11/2021 1:51:46 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[NGUOIDUNG](
	[MaND] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[TaiKhoan] [varchar](50) NOT NULL,
	[MatKhau] [varchar](50) NOT NULL,
	[DienThoai] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_NGUOIDUNG] PRIMARY KEY CLUSTERED 
(
	[MaND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[NHOM]    Script Date: 02/11/2021 1:51:06 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[NHOM](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[MaSV] [nvarchar](50) NOT NULL,
	[PhanChiaCongViec] [nvarchar](50) NOT NULL,
	[ThucHien] [varchar](50) NULL,
 CONSTRAINT [PK_NHOM] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[CONGTAC]    Script Date: 02/11/2021 1:50:51 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[CONGTAC](
	[MaDK] [int] IDENTITY(1,1) NOT NULL,
	[TimeGo] [date] NULL,
	[TimeEnd] [date] NULL,
	[NguoiChuanBi] [nvarchar](50) NULL,
	[Noidi] [nvarchar](50) NULL,
	[Noiden] [nvarchar](50) NULL,
	[ThanhPhan] [nvarchar](50) NULL,
	[SoKm] [int] NULL,
	[SoGhe] [int] NULL,
	[NoiDung] [nvarchar](max) NULL,
	[DonViChuTri] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](max) NULL,
	[MaND] [int] NULL,
	FOREIGN KEY([MaND]) REFERENCES  [dbo].[NGUOIDUNG]([MaND]),
  CONSTRAINT [PK_CONGTAC] PRIMARY KEY CLUSTERED 
(
	[MaDK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


---Bảng Nơi đến
CREATE TABLE [dbo].[NOIDEN](
	[STT] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar] (50) null,
	[Address] [nvarchar](max) NULL,

 CONSTRAINT [PK_NOIDEN] PRIMARY KEY CLUSTERED 
(
	[STT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

set dateformat DMY


insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận 1 ')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận 2 ')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận 3 ')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận 4 ')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận 7 ')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận Thủ Đức')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận Gò Vấp')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận Củ Chi')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận Phú Nhuận ')
insert NOIDEN values(N'Thành phố Hồ Chí Minh',N'Quận Tân Phú ')

insert NOIDEN values(N'Thành phố Hà Nội',N'Quận Ba Đình ')
insert NOIDEN values(N'Thành phố Hà Nội',N'Quận Cầu Giấy  ')
insert NOIDEN values(N'Thành phố Hà Nội',N'Quận Đống Đa  ')
insert NOIDEN values(N'Thành phố Hà Nội',N'Quận Hà Đông  ')
insert NOIDEN values(N'Thành phố Hà Nội',N'Quận Hoàn Kiếm  ')
insert NOIDEN values(N'Thành phố Hà Nội',N'Quận Tây Hồ   ')



insert NOIDEN values(N'Tỉnh Thanh Hóa',N'Thành phố Thanh Hóa  ')
insert NOIDEN values(N'Tỉnh Thanh Hóa',N'Huyện Đông Sơn   ')
insert NOIDEN values(N'Tỉnh Thanh Hóa',N'Huyện Quảng Xương   ')
insert NOIDEN values(N'Tỉnh Thanh Hóa',N'Huyện Hoằng Hóa   ')
insert NOIDEN values(N'Tỉnh Thanh Hóa',N'Huyện Hậu Lộc  ')

insert NOIDEN values(N'Tỉnh Đồng Nai',N'Thành phố Biên Hòa   ')
insert NOIDEN values(N'Tỉnh Đồng Nai',N'Thành phố Long Khánh   ')
insert NOIDEN values(N'Tỉnh Đồng Nai',N'Huyện Trảng Bom   ')
insert NOIDEN values(N'Tỉnh Đồng Nai',N'Huyện Thống Nhất   ')
insert NOIDEN values(N'Tỉnh Đồng Nai',N'Huyện Long Thành   ')







