﻿using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

namespace HomeBrewComp.Persistence
{
    public static class NHibernateSetup
    {
        public static ISessionFactory SessionFactory { get; private set; }

        private static Configuration configuration;
        private static readonly object sync = new object();

        public static void Configure(string connectionStringName)
        {
            if (configuration == null)
            {
                lock (sync)
                {
                    if (configuration == null)
                    {
                        configuration = DoConfigure(connectionStringName);
                        SessionFactory = configuration.BuildSessionFactory();
                    }
                }
            }
        }

        public static string Create(bool execute)
        {
            var script = new SchemaExport(configuration);

            var sb = new System.Text.StringBuilder();
            script.Create(a =>
            {
                sb.Append(a);
            }, false);

            script.Create(script: false, export: execute);
            return sb.ToString();
        }

        public static void Drop(bool execute)
        {
            var script = new SchemaExport(configuration);
            script.Drop(script: false, export: true);
        }

        private static Configuration DoConfigure(string connectionStringName)
        {
            var configuration = new Configuration();

            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionStringName = connectionStringName;
                db.Dialect<MsSql2008Dialect>();
            })
            .SessionFactory().GenerateStatistics();

            var mapper = new HomeBrewCompModelMapper();
            var mappings = mapper.CreateMappings();
            configuration.AddMapping(mappings);

            SchemaMetadataUpdater.QuoteTableAndColumns(configuration);
            return configuration;
        }
    }
}