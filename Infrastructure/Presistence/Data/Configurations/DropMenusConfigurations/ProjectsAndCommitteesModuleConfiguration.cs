namespace Presistence.Data.Configurations.DropMenusConfigurations
{
    public class ProjectsAndCommitteesModuleConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.HasData(

            #region Comitee Participation Degree

                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666661"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "رئيس مجلس الادارة",
                     ValueEn = "Chairman of the Board of Directors",
                     SortOrder = 1,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666662"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "رئيس اللجنة",
                     ValueEn = "Chairman of the Committee",
                     SortOrder = 2,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666663"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "مدير",
                     ValueEn = "Boss",
                     SortOrder = 3,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666664"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "منسق",
                     ValueEn = "Coordinator",
                     SortOrder = 4,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666665"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "مقرر",
                     ValueEn = "Decidor",
                     SortOrder = 5,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "مشرف",
                     ValueEn = "Supervisor",
                     SortOrder = 6,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666667"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "استشاري",
                     ValueEn = "Consultative",
                     SortOrder = 7,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666668"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "سكرتير",
                     ValueEn = "Secretary",
                     SortOrder = 8,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666669"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "مراجع",
                     ValueEn = "Revisor",
                     SortOrder = 9,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666670"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "عضو مجلس ادارة",
                     ValueEn = "Member of the Board of Directors",
                     SortOrder = 10,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666671"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "عضو مجلس تحرير",
                     ValueEn = "Editorial board member",
                     SortOrder = 11,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666672"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "عضو مؤسس",
                     ValueEn = "Founding member",
                     SortOrder = 12,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666673"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "عضو عامل",
                     ValueEn = "Active member",
                     SortOrder = 13,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666674"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "عضو",
                     ValueEn = "Member",
                     SortOrder = 14,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },
                 new Lookup
                 {
                     Id = Guid.Parse("66666666-6666-6666-6666-666666666675"),
                     Type = LookupTypes.ComiteeParticipationDegree.ToString(),
                     Key = "PARTICIPATIONTYPES",
                     ValueAr = "متحكم",
                     ValueEn = "Controller",
                     SortOrder = 15,
                     CreatedBy = "Helwan Faculty Portal System",
                     CreatedAt = new DateTime(2025, 11, 22)
                 },

            #endregion

            #region Comitee Types

               new Lookup
               {
                   Id = Guid.Parse("77777777-7777-7777-7777-777777777771"),
                   Type = LookupTypes.TypeofComitee.ToString(),
                   Key = "COMITEETYPES",
                   ValueAr = "لجان علمية",
                   ValueEn = "Scientific committees",
                   SortOrder = 1,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
               new Lookup
               {
                   Id = Guid.Parse("77777777-7777-7777-7777-777777777772"),
                   Type = LookupTypes.TypeofComitee.ToString(),
                   Key = "COMITEETYPES",
                   ValueAr = "جمعيات",
                   ValueEn = "Associations",
                   SortOrder = 2,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
               new Lookup
               {
                   Id = Guid.Parse("77777777-7777-7777-7777-777777777773"),
                   Type = LookupTypes.TypeofComitee.ToString(),
                   Key = "COMITEETYPES",
                   ValueAr = "لجان",
                   ValueEn = "Committees",
                   SortOrder = 3,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
               new Lookup
               {
                   Id = Guid.Parse("77777777-7777-7777-7777-777777777774"),
                   Type = LookupTypes.TypeofComitee.ToString(),
                   Key = "COMITEETYPES",
                   ValueAr = "اخرى",
                   ValueEn = "Other",
                   SortOrder = 4,
                   CreatedBy = "Helwan Faculty Portal System",
                   CreatedAt = new DateTime(2025, 11, 22)
               },
            #endregion

            #region Project Types

                     new Lookup
                     {
                         Id = Guid.Parse("88888888-8888-8888-8888-888888888881"),
                         Type = LookupTypes.ProjectType.ToString(),
                         Key = "PROJECTTYPES",
                         ValueAr = "بحثي",
                         ValueEn = "Research",
                         SortOrder = 1,
                         CreatedBy = "Helwan Faculty Portal System",
                         CreatedAt = new DateTime(2025, 11, 22)
                     },
                     new Lookup
                     {
                         Id = Guid.Parse("88888888-8888-8888-8888-888888888882"),
                         Type = LookupTypes.ProjectType.ToString(),
                         Key = "PROJECTTYPES",
                         ValueAr = "هندسي",
                         ValueEn = "Geometric",
                         SortOrder = 2,
                         CreatedBy = "Helwan Faculty Portal System",
                         CreatedAt = new DateTime(2025, 11, 22)
                     },
                     new Lookup
                     {
                         Id = Guid.Parse("88888888-8888-8888-8888-888888888883"),
                         Type = LookupTypes.ProjectType.ToString(),
                         Key = "PROJECTTYPES",
                         ValueAr = "جودة",
                         ValueEn = "Quality",
                         SortOrder = 3,
                         CreatedBy = "Helwan Faculty Portal System",
                         CreatedAt = new DateTime(2025, 11, 22)
                     },
                     new Lookup
                     {
                         Id = Guid.Parse("88888888-8888-8888-8888-888888888884"),
                         Type = LookupTypes.ProjectType.ToString(),
                         Key = "PROJECTTYPES",
                         ValueAr = "خارجي",
                         ValueEn = "External",
                         SortOrder = 4,
                         CreatedBy = "Helwan Faculty Portal System",
                         CreatedAt = new DateTime(2025, 11, 22)
                     },

            #endregion

            #region Project Roles

                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999091"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "مدير مشروع",
                        ValueEn = "Project manager",
                        SortOrder = 1,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999092"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "مدير تنفيذي",
                        ValueEn = "Executive Director",
                        SortOrder = 2,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999093"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "نائب مدير تنفيذي",
                        ValueEn = "Deputy Executive Director",
                        SortOrder = 3,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999094"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "باحث رئيسي",
                        ValueEn = "Principal researcher",
                        SortOrder = 4,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999095"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "باحث مشارك",
                        ValueEn = "Contributer researcher",
                        SortOrder = 5,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999096"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "مستشار",
                        ValueEn = "Consultant",
                        SortOrder = 6,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("99999999-9999-9999-9999-999999999097"),
                        Type = LookupTypes.ProjectRole.ToString(),
                        Key = "PROJECTROLES",
                        ValueAr = "متحكم",
                        ValueEn = "Controller",
                        SortOrder = 7,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
            #endregion

            #region Magazine Participation Role

                    new Lookup
                    {
                        Id = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                        Type = LookupTypes.MagazineParticipationRole.ToString(),
                        Key = "MAGAZINEPARTICIPATIONROLE",
                        ValueAr = "رئيس تحرير",
                        ValueEn = "Editor-in-Chief",
                        SortOrder = 1,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("10101010-1010-1010-1010-101010101011"),
                        Type = LookupTypes.MagazineParticipationRole.ToString(),
                        Key = "MAGAZINEPARTICIPATIONROLE",
                        ValueAr = "مدير تحرير",
                        ValueEn = "Editorial Director",
                        SortOrder = 2,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("10101010-1010-1010-1010-101010101012"),
                        Type = LookupTypes.MagazineParticipationRole.ToString(),
                        Key = "MAGAZINEPARTICIPATIONROLE",
                        ValueAr = "نائب تحرير",
                        ValueEn = "Deputy editor",
                        SortOrder = 3,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("10101010-1010-1010-1010-101010101013"),
                        Type = LookupTypes.MagazineParticipationRole.ToString(),
                        Key = "MAGAZINEPARTICIPATIONROLE",
                        ValueAr = "عضو",
                        ValueEn = "Member",
                        SortOrder = 4,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("10101010-1010-1010-1010-101010101014"),
                        Type = LookupTypes.MagazineParticipationRole.ToString(),
                        Key = "MAGAZINEPARTICIPATIONROLE",
                        ValueAr = "محرر",
                        ValueEn = "Editor",
                        SortOrder = 5,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    },
                    new Lookup
                    {
                        Id = Guid.Parse("10101010-1010-1010-1010-101010101015"),
                        Type = LookupTypes.MagazineParticipationRole.ToString(),
                        Key = "MAGAZINEPARTICIPATIONROLE",
                        ValueAr = "محكم",
                        ValueEn = "ReFree",
                        SortOrder = 6,
                        CreatedBy = "Helwan Faculty Portal System",
                        CreatedAt = new DateTime(2025, 11, 22)
                    }
                    #endregion

            );
        }
    }
}
