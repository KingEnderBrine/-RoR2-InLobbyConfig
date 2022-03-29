using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ThunderKit.Core.Attributes;
using ThunderKit.Core.Paths;
using ThunderKit.Core.Pipelines;
using UnityEngine;
using UnityEngine.Serialization;

[PipelineSupport(typeof(Pipeline))]
public class DeleteFiles : FlowPipelineJob
{
    public bool IsFatal;
    public string Path;
    public string SearchPattern;
    public SearchOption SearchOption;

    protected override Task ExecuteInternal(Pipeline pipeline)
    {
        var path = Path.Resolve(pipeline, this);
        var files = Array.Empty<string>();
        try
        {
            if (!Directory.Exists(path))
            {
                return Task.CompletedTask;
            }

            files = Directory.GetFiles(path, SearchPattern, SearchOption);
        }
        catch
        {
            if (IsFatal)
                throw;
        }

        foreach (var file in files)
        {
            try
            {
                pipeline.Log(LogLevel.Information, $"Deleting {file}");
                File.Delete(file);
            }
            catch
            {
                if (IsFatal)
                    throw;
            }
        }

        return Task.CompletedTask;
    }
}
