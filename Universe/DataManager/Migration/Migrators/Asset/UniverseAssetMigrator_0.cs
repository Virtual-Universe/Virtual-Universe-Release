/*
 * Copyright (c) Contributors, http://virtual-planets.org/, http://whitecore-sim.org/, http://aurora-sim.org, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Virtual-Universe Project nor the
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
using Universe.Framework.Utilities;

namespace Universe.DataManager.Migration.Migrators.Asset
{
    public class UniverseAssetMigrator_0 : Migrator
    {
        public UniverseAssetMigrator_0()
        {
            Version = new Version(0, 0, 0);
            MigrationName = "BlackholeAsset";

            schema = new List<SchemaDefinition>();

			AddSchema("universeassets_A", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_B", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_C", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_D", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_E", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_F", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_0", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_1", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_2", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_3", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_4", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_5", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_6", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_7", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_8", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_9", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024),
                ColDef("parent_id", ColumnTypes.String36)
                                            ), IndexDefs(
                                                IndexDef(new string[4] {"id", "hash_code", "creator_id", "parent_id"},
                                                         IndexType.Primary)
                                                   ));

			AddSchema("universeassets_old", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("parent_id", ColumnTypes.String36),
                ColDef("creator_id", ColumnTypes.String36),
                ColDef("name", ColumnTypes.String64),
                ColDef("description", ColumnTypes.String128),
                ColDef("asset_type", ColumnTypes.Integer11),
                ColDef("create_time", ColumnTypes.Integer11),
                ColDef("access_time", ColumnTypes.Integer11),
                ColDef("asset_flags", ColumnTypes.String64),
                ColDef("owner_id", ColumnTypes.String36),
                ColDef("host_uri", ColumnTypes.String1024)
                                              ), IndexDefs(
                                                  IndexDef(
                                                      new string[4] {"id", "hash_code", "parent_id", "creator_id"},
                                                      IndexType.Primary)
                                                     ));

			AddSchema("universeassets_tasks", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("task_type", ColumnTypes.String64),
                ColDef("task_values", ColumnTypes.String36)
                                                ), IndexDefs(
                                                    IndexDef(new string[1] {"id"}, IndexType.Primary)
                                                       ));

			AddSchema("universeassets_temp", ColDefs(
                ColDef("id", ColumnTypes.String36),
                ColDef("hash_code", ColumnTypes.String64),
                ColDef("creator_id", ColumnTypes.String36)
                                               ), IndexDefs(
                                                   IndexDef(new string[3] {"id", "hash_code", "creator_id"},
                                                            IndexType.Primary)
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
    }
}