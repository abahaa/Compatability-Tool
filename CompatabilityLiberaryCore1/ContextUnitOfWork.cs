using System;
using System.Collections.Generic;
using System.Text;

namespace CompatabilityLiberaryCore1
{
    public class ContextUnitOfWork
    {
        private string _connectionString;
        private CompatabilityRep _compatabilityRep;
        private ReleasesRep _releasesRep;

        public ContextUnitOfWork(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public CompatabilityRep compatabilityRep
        {
            get
            {
                if (_compatabilityRep == null)
                {
                    return new CompatabilityRep(_connectionString);
                }
                else
                    return _compatabilityRep;
            }
        }
        public ReleasesRep releasesRep
        {
            get
            {
                if (_releasesRep == null)
                {
                    return new ReleasesRep(_connectionString);
                }
                else
                    return _releasesRep;
            }
        }
    }
}
