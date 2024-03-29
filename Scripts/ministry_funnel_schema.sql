USE [master]
GO
/****** Object:  Database [MinistryFunnel_Schema]    Script Date: 8/1/2021 9:36:40 AM ******/
CREATE DATABASE [MinistryFunnel_Schema]

GO
ALTER DATABASE [MinistryFunnel_Schema] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MinistryFunnel_Schema].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MinistryFunnel_Schema] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET ARITHABORT OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET RECOVERY FULL 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET  MULTI_USER 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MinistryFunnel_Schema] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MinistryFunnel_Schema] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MinistryFunnel_Schema', N'ON'
GO
ALTER DATABASE [MinistryFunnel_Schema] SET QUERY_STORE = OFF
GO
USE [MinistryFunnel_Schema]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 8/1/2021 9:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Approvals]    Script Date: 8/1/2021 9:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Approvals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_Approvals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 8/1/2021 9:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_Campus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Frequency]    Script Date: 8/1/2021 9:36:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Frequency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_Frequency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funnels]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funnels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTIme] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_Funnels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LevelOfImportance]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LevelOfImportance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_LevelOfImportance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogEvent]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogEvent](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[User] [nvarchar](200) NULL,
	[ApplicationName] [nvarchar](200) NULL,
	[ClassName] [nvarchar](200) NULL,
	[Action] [nvarchar](200) NULL,
	[Event] [nvarchar](200) NULL,
	[SearchText] [nvarchar](500) NULL,
	[Notes] [nvarchar](max) NULL,
	[Created] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ministry]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ministry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Event] [nvarchar](200) NULL,
	[Purpose] [nvarchar](max) NULL,
	[DesiredOutcome] [nvarchar](max) NULL,
	[Archived] [bit] NULL,
	[PracticeId] [int] NULL,
	[FunnelId] [int] NULL,
	[LocationId] [int] NULL,
	[CampusId] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[FrequencyId] [int] NULL,
	[KidCare] [bit] NULL,
	[LevelOfImportanceId] [int] NULL,
	[ApprovalId] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[MinistryOwnerId] [int] NULL,
 CONSTRAINT [PK_Ministry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MinistryOwners]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MinistryOwners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.MinistryOwners] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Practices]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Practices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_Practices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceInvolvement]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceInvolvement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_ResourceInvolvement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceInvolvementRelationship]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceInvolvementRelationship](
	[MinistryId] [int] NOT NULL,
	[ResourceInvolvementId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MinistryId] ASC,
	[ResourceInvolvementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpInOut]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpInOut](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[Archived] [bit] NULL,
 CONSTRAINT [PK_UpInOUt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpInOutRelationship]    Script Date: 8/1/2021 9:36:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpInOutRelationship](
	[MinistryId] [int] NOT NULL,
	[UpInOutId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MinistryId] ASC,
	[UpInOutId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_Approval] FOREIGN KEY([ApprovalId])
REFERENCES [dbo].[Approvals] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_Approval]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_Campus] FOREIGN KEY([CampusId])
REFERENCES [dbo].[Campus] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_Campus]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_Frequency] FOREIGN KEY([FrequencyId])
REFERENCES [dbo].[Frequency] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_Frequency]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_Funnel] FOREIGN KEY([FunnelId])
REFERENCES [dbo].[Funnels] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_Funnel]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_LevelOfImportance] FOREIGN KEY([LevelOfImportanceId])
REFERENCES [dbo].[LevelOfImportance] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_LevelOfImportance]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_Location]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_MinistryOwner] FOREIGN KEY([MinistryOwnerId])
REFERENCES [dbo].[MinistryOwners] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_MinistryOwner]
GO
ALTER TABLE [dbo].[Ministry]  WITH CHECK ADD  CONSTRAINT [FK_Practice] FOREIGN KEY([PracticeId])
REFERENCES [dbo].[Practices] ([Id])
GO
ALTER TABLE [dbo].[Ministry] CHECK CONSTRAINT [FK_Practice]
GO
ALTER TABLE [dbo].[ResourceInvolvementRelationship]  WITH CHECK ADD FOREIGN KEY([ResourceInvolvementId])
REFERENCES [dbo].[ResourceInvolvement] ([Id])
GO
ALTER TABLE [dbo].[UpInOutRelationship]  WITH CHECK ADD FOREIGN KEY([MinistryId])
REFERENCES [dbo].[Ministry] ([Id])
GO
ALTER TABLE [dbo].[UpInOutRelationship]  WITH CHECK ADD FOREIGN KEY([UpInOutId])
REFERENCES [dbo].[UpInOut] ([Id])
GO
USE [master]
GO
ALTER DATABASE [MinistryFunnel_Schema] SET  READ_WRITE 
GO
