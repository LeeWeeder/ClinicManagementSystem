USE [ClinicManagementSystemDB]
GO

/****** Object:  Table [dbo].[Appointment]    Script Date: 5/29/2024 3:28:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Appointment](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentPatientCaseId] [int] NOT NULL,
	[AppointmentAttendingStaffId] [int] NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentStartTime] [time](7) NOT NULL,
	[AppointmentEndTime] [time](7) NULL,
	[AppointmentType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Patient] FOREIGN KEY([AppointmentPatientCaseId])
REFERENCES [dbo].[PatientCase] ([PatientCaseId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Patient]
GO

