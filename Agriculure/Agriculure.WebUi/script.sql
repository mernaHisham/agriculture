USE [master]
GO
/****** Object:  Database [agricultureDB]    Script Date: 2/20/2020 4:37:33 AM ******/
CREATE DATABASE [agricultureDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'agricultureDB', FILENAME = N'E:\StartUp\grad-project\solution\db\agricultureDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'agricultureDB_log', FILENAME = N'E:\StartUp\grad-project\solution\db\agricultureDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [agricultureDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [agricultureDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [agricultureDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [agricultureDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [agricultureDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [agricultureDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [agricultureDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [agricultureDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [agricultureDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [agricultureDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [agricultureDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [agricultureDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [agricultureDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [agricultureDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [agricultureDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [agricultureDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [agricultureDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [agricultureDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [agricultureDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [agricultureDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [agricultureDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [agricultureDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [agricultureDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [agricultureDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [agricultureDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [agricultureDB] SET  MULTI_USER 
GO
ALTER DATABASE [agricultureDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [agricultureDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [agricultureDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [agricultureDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [agricultureDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [agricultureDB]
GO
/****** Object:  Table [dbo].[Farmer]    Script Date: 2/20/2020 4:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Farmer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FarmerName] [nvarchar](100) NOT NULL,
	[FarmerAddress] [nvarchar](200) NULL,
	[FarmerEmail] [nvarchar](100) NOT NULL,
	[FarmerRole] [nvarchar](50) NULL,
	[FarmerLiecnse] [nvarchar](100) NULL,
	[FarmerNID] [nvarchar](100) NULL,
	[FarmerSID] [bigint] NOT NULL,
 CONSTRAINT [PK_Farmer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FarmSupplier]    Script Date: 2/20/2020 4:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FarmSupplier](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FarmerSName] [nvarchar](100) NOT NULL,
	[FarmerSAddress] [nvarchar](200) NULL,
	[FarmerSRole] [nvarchar](50) NULL,
	[FarmerSEmail] [nvarchar](100) NULL,
	[FarmerSLicense] [nvarchar](50) NULL,
	[FarmerSNID] [nvarchar](50) NULL,
 CONSTRAINT [PK_FarmSupplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Logistics]    Script Date: 2/20/2020 4:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logistics](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[LogisticsName] [nvarchar](100) NOT NULL,
	[LogisticsAddress] [nvarchar](200) NULL,
	[LogisticsRole] [nvarchar](50) NULL,
	[LogisticsEmail] [nvarchar](100) NULL,
	[LogisticsLicense] [nvarchar](100) NULL,
	[LogisticsNID] [nvarchar](100) NULL,
	[ProducerID] [bigint] NOT NULL,
 CONSTRAINT [PK_Logistics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producer]    Script Date: 2/20/2020 4:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProducerName] [nvarchar](100) NOT NULL,
	[ProducerAddress] [nvarchar](200) NULL,
	[ProducerRole] [nvarchar](50) NULL,
	[ProducerEmail] [nvarchar](100) NULL,
	[ProducerLicense] [nvarchar](100) NULL,
	[ProducerNID] [nvarchar](100) NULL,
	[FarmerID] [bigint] NOT NULL,
 CONSTRAINT [PK_Producer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Retailer]    Script Date: 2/20/2020 4:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Retailer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RetailerName] [nvarchar](100) NOT NULL,
	[RetailerAddress] [nvarchar](200) NULL,
	[RetailerRole] [nvarchar](50) NULL,
	[RetailerEmail] [nvarchar](100) NULL,
	[RetailerLicense] [nvarchar](100) NULL,
	[RetailerNID] [nvarchar](100) NULL,
	[ProducerID] [bigint] NOT NULL,
	[LogisticsID] [bigint] NOT NULL,
 CONSTRAINT [PK_Retailer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 2/20/2020 4:37:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ProducerID] [bigint] NOT NULL,
	[LogisticsID] [bigint] NOT NULL,
	[FarmerSID] [bigint] NOT NULL,
	[FarmerID] [bigint] NOT NULL,
	[RetailerID] [bigint] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Farmer]  WITH CHECK ADD  CONSTRAINT [FK_Farmer_FarmSupplier] FOREIGN KEY([FarmerSID])
REFERENCES [dbo].[FarmSupplier] ([ID])
GO
ALTER TABLE [dbo].[Farmer] CHECK CONSTRAINT [FK_Farmer_FarmSupplier]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_Logistics_Producer] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producer] ([ID])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_Logistics_Producer]
GO
ALTER TABLE [dbo].[Producer]  WITH CHECK ADD  CONSTRAINT [FK_Producer_Farmer] FOREIGN KEY([FarmerID])
REFERENCES [dbo].[Farmer] ([ID])
GO
ALTER TABLE [dbo].[Producer] CHECK CONSTRAINT [FK_Producer_Farmer]
GO
ALTER TABLE [dbo].[Retailer]  WITH CHECK ADD  CONSTRAINT [FK_Retailer_Logistics] FOREIGN KEY([LogisticsID])
REFERENCES [dbo].[Logistics] ([ID])
GO
ALTER TABLE [dbo].[Retailer] CHECK CONSTRAINT [FK_Retailer_Logistics]
GO
ALTER TABLE [dbo].[Retailer]  WITH CHECK ADD  CONSTRAINT [FK_Retailer_Producer] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producer] ([ID])
GO
ALTER TABLE [dbo].[Retailer] CHECK CONSTRAINT [FK_Retailer_Producer]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Farmer] FOREIGN KEY([FarmerID])
REFERENCES [dbo].[Farmer] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Farmer]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_FarmSupplier] FOREIGN KEY([FarmerSID])
REFERENCES [dbo].[FarmSupplier] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_FarmSupplier]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Logistics] FOREIGN KEY([LogisticsID])
REFERENCES [dbo].[Logistics] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Logistics]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Producer] FOREIGN KEY([ProducerID])
REFERENCES [dbo].[Producer] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Producer]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Retailer] FOREIGN KEY([RetailerID])
REFERENCES [dbo].[Retailer] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Retailer]
GO
USE [master]
GO
ALTER DATABASE [agricultureDB] SET  READ_WRITE 
GO
