using MinistryFunnel.Data;
using MinistryFunnel.Models;
using MinistryFunnel.Repository.Interfaces;
using MinistryFunnel.Service;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MinistryFunnel.Repository
{
    public class UpInOutRelationshipRepository : IUpInOutRelatinshipRepository
    {
        private MinistryFunnelContext db = new MinistryFunnelContext();
        private readonly ILoggerService _loggerService;

        public UpInOutRelationshipRepository()
        {
            _loggerService = new LoggerService();
        }

        public IQueryable<UpInOutRelationship> GetUpInOutRelationships()
        {
            return db.UpInOutRelaionship;
        }

        public IQueryable<UpInOutRelationship> GetUpInOutRelationshipsByMinistryId(int ministryId)
        {
            return db.UpInOutRelaionship.Where(x => x.MinistryId == ministryId);
        }
    }
}