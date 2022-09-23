using System;
using System.Collections.Generic;
using System.CommandLine;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace RealitSystem_CLI.Commands
{
    [Verb("scene", HelpText = "Setup the scene")]
    internal class SceneCommands : RealitCommand
    {
        [Option("player-pos", Required = false, HelpText = "Player position on startup")]
        public string PlayerPos 
        {
            set
            {
                if (Vector3.TrySerialize(value, out Vector3 vector3))
                {
                    AddModification(() => {
                            RealitBuilder.Current.PlayerPosition = vector3;
                            RealitBuilder.Current.Dirty = true;
                        });
                }
                else
                {
                    AddFailureMessage("player-pos is not valid. Value should be x/y/z.");
                }
            }
        }

        [Option("player-rot", Required = false, HelpText = "Player orientation on startup")]
        public string PlayerRot
        {
            set
            {
                if (Vector3.TrySerialize(value, out Vector3 vector3))
                {
                    AddModification(() =>
                        {
                            RealitBuilder.Current.PlayerRotation = vector3;
                            RealitBuilder.Current.Dirty = true;
                        });
                }
                else
                {
                    AddFailureMessage("player-rot is not valid. Value should be x/y/z.");
                }
            }
        }

        [Option('m', "model-path", Required = false, HelpText = "Location of the model that will be used")]
        public string ModelPath
        {
            set
            {
                if (File.Exists(value))
                {
                    AddModification(() =>
                        {
                            RealitBuilder.Current.ModelPath = value;
                            RealitBuilder.Current.Dirty = true;
                        });
                }
                else
                {
                    AddFailureMessage($"Couldn't find model at {value}");
                }
            }
        }

        [Option("model-appertures",
        Separator = '|', 
        Required = false, 
        HelpText = "Define apperture with path and submesh index. Example : ObjectA/ObjectB.2|ObjectA/ObjectC.1")]
        public IEnumerable<string> appertures
        {
            set
            {
                AddModification(() =>
                {
                    RealitBuilder.Current.Appertures = value.ToArray();
                    RealitBuilder.Current.Dirty = true;
                });
            }
        }
    }
}
