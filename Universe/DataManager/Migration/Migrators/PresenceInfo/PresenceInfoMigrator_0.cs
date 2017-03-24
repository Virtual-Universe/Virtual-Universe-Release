/*
 * Copyright (c) Contributors, http://virtual-planets.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 * For an explanation of the license of each contributor and the content it 
 * covers please see the Licenses directory.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual Universe Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using Universe.DataManager.Migration;
using Universe.Framework.Utilities;

namespace Universe.Modules.Ban
{
    /// <summary>
    ///     This update just clears the table out
    /// </summary>
    public class PresenceInfoMigrator_0 : Migrator
    {
        public PresenceInfoMigrator_0()
        {
            Version = new Version(0, 0, 0);
            MigrationName = "PresenceInfo";

            Schema = new List<SchemaDefinition>();

            AddSchema("baninfo", ColDefs(
                ColDef("AgentID", ColumnTypes.String50),
                ColDef("Flags", ColumnTypes.String50),
                ColDef("KnownAlts", ColumnTypes.Text),
                ColDef("KnownID0s", ColumnTypes.Text),
                ColDef("KnownIPs", ColumnTypes.Text),
                ColDef("KnownMacs", ColumnTypes.Text),
                ColDef("KnownViewers", ColumnTypes.Text),
                ColDef("LastKnownID0", ColumnTypes.String50),
                ColDef("LastKnownIP", ColumnTypes.String50),
                ColDef("LastKnownMac", ColumnTypes.String50),
                ColDef("LastKnownViewer",ColumnTypes.String255),
                ColDef("Platform", ColumnTypes.String50)
                                     ), IndexDefs(
                                         IndexDef(new string[1] {"AgentID"}, IndexType.Primary)
                                            ));
        }

        protected override void DoCreateDefaults(IDataConnector genericData)
        {
            EnsureAllTablesInSchemaExist(genericData);
        }

        protected override bool DoValidate(IDataConnector genericData)
        {
            return TestThatAllTablesValidate(genericData);
        }

        protected override void DoMigrate(IDataConnector genericData)
        {
            DoCreateDefaults(genericData);
        }

        protected override void DoPrepareRestorePoint(IDataConnector genericData)
        {
            CopyAllTablesToTempVersions(genericData);
        }

        public override void FinishedMigration(IDataConnector genericData)
        {
            genericData.Delete("baninfo", null);
        }
    }
}
