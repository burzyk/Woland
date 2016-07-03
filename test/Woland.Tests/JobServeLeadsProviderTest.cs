namespace Woland.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Business;
    using Domain;
    using Microsoft.Practices.Unity;
    using Xunit;

    public class JobServeLeadsProviderTest : BaseTest
    {
        [Fact]
        public void GetTopThreeJobs()
        {
            using (var container = this.CreateContainer())
            {
                container.RegisterInstance<IWebClient>(new StaticClient());
                var provider = container.Resolve<JobServeLeadsProvider>();
                var result = provider.GetLatestLeads("C#", "London", 0, 3).ToList();

                Assert.Equal(3, result.Count);

                Assert.Equal("JNC Recruitment Ltd", result[0].AgencyName);
                Assert.Equal("<p><strong> IT Business/Solutions Analyst - London - £400 - 500 p/day </strong></p> <p> My client has an urgent requirement for an IT Business/Solutions Analyst who can work on a variety of key projects and initiatives to help them understand the exact needs and implications to the business of the work being undertaken and to provide detailed and accurate solutions back to the client/stakeholder. </p> <p> It is critical that all applicants have an analytical approach to their work, as well as strong knowledge of the whole project delivery life cycle including planning, development, testing and implementation. </p> <p> Key Skills/Experience Required:- </p> <ul> <li> Strong Business/Solutions Analysis experience </li> <li> A strong analytical approach to work </li> <li> Experience of the Project Delivery Life Cycle </li> <li> Experience of the SDLC (Software Development Life Cycle) </li> <li> Ability to liaise with stakeholders in order to elicit, analyse, communicate customer requirements </li> <li> Strong requirement gathering experience </li> <li> Ability to perform detailed business and systems process analysis </li> <li> Excellent documentation skills including; impact analysis, wireframes, story-boards, technical/functional specifications etc. </li> <li> Any previous experience of working on Digital or CRM projects would be highly desirable </li> <li> Excellent communication skills and experience of dealing with senior stakeholders </li> <li> Ability to manage meetings, reviews, release planning and workshops as required </li> <li> Experience of SQL, MATLAB, SharePoint, C#, .NET </li> <li> Proven Agile experience </li> </ul> <p> In line with the Conduct Regulations 2003, when advertising permanent vacancies JNC Recruitment are acting as an Employment Agency, and when advertising temporary/contract vacancies JNC Recruitment are acting as an Employment Business. </p>", result[0].Body);
                Assert.Equal("Jon Hunter", result[0].FullName);
                Assert.Equal(500, result[0].MaxRate);
                Assert.Equal(400, result[0].MinRate);
                Assert.Equal(new DateTime(2016, 07, 01, 22, 17, 25), result[0].PostedTimestamp);
                Assert.Equal("C#", result[0].SearchDetails.Keywords);
                Assert.Equal("London", result[0].SearchDetails.Location);
                Assert.Equal("http://www.jobserve.com/Enpzg", result[0].SourceUrl);
                Assert.Equal("01273 774738", result[0].Telephone);
                Assert.Equal("IT Business/Solutions Analyst - London - £400 - 500 p/day", result[0].Title);
                Assert.Equal("JobServe", result[0].SourceName);

                Assert.Equal("Information Technology Services", result[1].AgencyName);
                Assert.Equal("<p><strong><u>Technical Lead Developer - Cognos, Interaction</u></strong></p><p>Technical Lead Developer - Cognos, Interaction - required by global Law firm to play major role in the upgrade of Cognos and Interaction. </p><p> Key Skills Required:</p><ul> <li>Previous experience working in Legal sector with Cognos, Interaction, Thompson Reuters Elite, Worksite or Aderant Expert</li><li>Strong experience and understanding of SQL Server.</li><li>Prior experience with .NET, C#, HTML, CSS, JavaScript,</li><li>jQuery or JSON would also be highly advantageous.</li><li>Able to effectively communicate with a range of different people. </li><li>Experience with engaging and communication with 3rd party vendors.</li><li>Solution design, development and maintenance experience in a multiple office global organisation.</li></ul> <p> This is a technical lead role which will entail of liaising with 3rd party vendors and different teams within the business. The firm is looking for two Lead Developers to help play a key role in the upgrade of their Cognos and Interaction software.</p><p>This is a fantastic opportunity to work for a global Law firm with offices all around the continent.</p><p> <strong><u>Technical Lead Developer - Cognos, Interaction</u></strong></p>", result[1].Body);
                Assert.Equal("Connor Morris", result[1].FullName);
                Assert.Equal(null, result[1].MaxRate);
                Assert.Equal(null, result[1].MinRate);
                Assert.Equal(new DateTime(2016, 07, 01, 20, 10, 02), result[1].PostedTimestamp);
                Assert.Equal("C#", result[1].SearchDetails.Keywords);
                Assert.Equal("London", result[1].SearchDetails.Location);
                Assert.Equal("http://www.jobserve.com/Enpue", result[1].SourceUrl);
                Assert.Equal(null, result[1].Telephone);
                Assert.Equal("Technical Lead Developer - Cognos", result[1].Title);
                Assert.Equal("JobServe", result[1].SourceName);

                Assert.Equal("Apollo Solutions Ltd", result[2].AgencyName);
                Assert.Equal("<p>An Application Security Analyst is required to join one of my clients who are a leading global bank. The role will focus on integrating security practices within IT development teams, and supporting applications comply with the Application Security Baseline. This is an exciting opportunity to work with interesting security challenges in an environment with many different development platforms, communications technologies, and advanced trading systems.<br><br>The role encompasses a number of activities & responsibilities:<br><br>*To promote and support Source Code Analysis (HP/Fortify 360) within the Software Development Lifecycle of development teams. Will involve working with developers to integrate SCA into their build environments and to assist with the identification, tracking, and remediation of vulnerabilities.<br><br>*Organise and manage the penetration testing of Internet facing applications using 3rd party vendors, and ideally have the skills to perform some level of penetration testing independently on request.<br><br>*To provide expertise on discovered vulnerabilities and to mediate/arbitrate disputes between developers and an offshore security testing team on the criticality of vulnerabilities.<br><br>*To drive, track, and assist application development teams comply with the Application Security baseline. Work with development teams on subjects such as strong authentication, encryption, data protection/leakage, etc.<br><br>*To strengthen development practices and improve overall development security through the highlighting of good practices and development methodologies.<br> <br>*Provide guidance on security tools and resources, performing on-going evaluation of developer training material and communication based on repeat vulnerabilities. <br><br>*Monitor public sources of vulnerability information for anything that might affect the division's applications or the corporate network.<br><br>*Maintain an internal vulnerability tracker with up to date information regarding any historical and current vulnerabilities. Contribute to the internal Wiki with relevant information for both the IT Security Team and the wider developer community.<br><br>Essential Skills<br><br>*Excellent understanding of development security and its implementation in systems: identification, authentication, access control and provisioning, alignment of jurisdiction to business process<br>*Familiarity with common security vulnerabilities (eg OWASP Top 10)<br>*Strong technical skills required to understand vulnerabilities in detail and how to resolve/mitigate them. <br>*Excellent knowledge of programming best practices, design patterns, etc.<br>*Excellent problem solving skills, being able to develop approaches to complex technology and strategy problems, building consensus across diverse interest groups and working within constraints of practical delivery yet able to think beyond the requirements of immediate issues.<br>*Well-developed written communication skills with the ability to summarise key issues, conclusions and recommendations in report form. Target audiences will include regulatory authorities and internal/external auditors.<br>*The candidate will be a forward thinking individual with the ability to look beyond immediate problems and issues, but with a solid practical delivery focus.<br>*Highly skilled and able to demonstrate value to the development community at a practical level, working alongside developers, production staff and technical architects on a collaborative basis<br>*The ability to manage independent responsibilities and projects while working closely with the security, architecture and development communities; the candidate must be well organised, self motivating and a good communicator<br>*A pragmatist with the strength of character to lead divergent interests to common ground and the best outcome <br>*Able to communicate effectively across a wide range of seniorities from entry level developer to senior management.<br>*A technologist, with an interest in emergent technologies and practices, with the ability to understand and communicate how these could fit within the technology estate<br>*Approachable and willing to share their expertise and experience in order to assist the development of teams and individuals<br><br>Desirable<br><br>*Development experience, preferably in Microsoft Visual Studio, .NET and Java<br>*Experience of specific security products and technologies: CA Siteminder, 2 factor authentication, Kerberos/SAML authentication solutions<br>*Experience of the development lifecycle within .NET, C# and/or Java projects<br>*Hands-on penetration testing experience <br>*Experience with source code analysis products (HP/Fortify)<br>*Knowledge of Web Application Firewalls: how to apply them and to define effective custom rules <br>*Competent in technical interviewing<br>*Familiarity with product adoption lifecycles, with an understanding of the different methods technologies, products and approaches can be introduced to an enterprise and the merits of each</p>", result[2].Body);
                Assert.Equal("Jason Henderson", result[2].FullName);
                Assert.Equal(550, result[2].MaxRate);
                Assert.Equal(500, result[2].MinRate);
                Assert.Equal(new DateTime(2016, 07, 01, 18, 46, 57), result[2].PostedTimestamp);
                Assert.Equal("C#", result[2].SearchDetails.Keywords);
                Assert.Equal("London", result[2].SearchDetails.Location);
                Assert.Equal("http://www.jobserve.com/Eno7L", result[2].SourceUrl);
                Assert.Equal("020 3167 3167", result[2].Telephone);
                Assert.Equal("Application Security Analyst (Security Developer)", result[2].Title);
                Assert.Equal("JobServe", result[2].SourceName);
            }
        }

        private class StaticClient : IWebClient
        {
            private readonly Dictionary<string, string> requests = new Dictionary<string, string>
            {
                { "http://www.jobserve.com//gb/en/mob/jobsearch", JobServeData.MainResponse },
                { "http://www.jobserve.com//gb/en/mob/jobsearch/results/37B6233495B9238815?Page=2", JobServeData.ResultPage2 },
                { "http://www.jobserve.com//gb/en/mob/jobsearch/results/37B6233495B9238815?Page=3", JobServeData.ResultPage3 },
                { "http://www.jobserve.com//gb/en/mob/jobsearch/results/37B6233495B9238815?Page=4", JobServeData.ResultPage4 },
                { "http://www.jobserve.com//gb/en/mob/jobsearch/results/37B6233495B9238815?Page=5", JobServeData.ResultPage5 },
                { "http://www.jobserve.com//gb/en/mob/job/3842279F367B28C9", JobServeData.JobLead1 },
                { "http://www.jobserve.com//gb/en/mob/job/F5C2D146F3690E1C", JobServeData.JobLead2 },
                { "http://www.jobserve.com//gb/en/mob/job/FF2CC047F505C68D", JobServeData.JobLead3 },
            };

            public string Get(string url)
            {
                return this.requests[url];
            }

            public string Post(string url, HttpContent content)
            {
                return this.requests[url];
            }
        }
    }
}
