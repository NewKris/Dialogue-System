using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VirtualDeviants.Editor.DialogueAuthor.Graph;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
    public static class GraphViewToAssetConverter {
        public static void WriteToGraphAsset(GraphAsset graphAsset, DialogueGraphView graph) {
            graphAsset.nodes = SerializeNodes(graph);
            graphAsset.groups = SerializeGroup(graph);
        }
        
        private static SerializedGraphNode[] SerializeNodes(DialogueGraphView graph) {
            GraphNode[] nodes = graph.nodes.Where(node => node is GraphNode).Cast<GraphNode>().ToArray();
            
            return nodes.Select(node => {
                int[] connections = node.GetConnections().Select(connectedNode => {
                    if (connectedNode == null) {
                        return -1;
                    }
					
                    return Array.IndexOf(nodes, connectedNode);
                }).ToArray();

                return new SerializedGraphNode() {
                    template = node.Template,
                    connections = connections,
                    position = node.Position
                };
            }).ToArray();
        }

        private static SerializedGraphGroup[] SerializeGroup(DialogueGraphView graph) {
            Group[] groups = graph.graphElements.Where(group => group is Group).Cast<Group>().ToArray();
            GraphNode[] nodes = graph.nodes.Where(node => node is GraphNode).Cast<GraphNode>().ToArray();
            
            return groups.Select(group => {
                GraphNode[] containedNodes = group.containedElements
                    .Where(node => node is GraphNode)
                    .Cast<GraphNode>()
                    .ToArray();
                
                return new SerializedGraphGroup() {
                    groupTitle = group.title,
                    containedNodes = containedNodes.Select(node => Array.IndexOf(nodes, node)).ToArray()
                };
            }).ToArray();
        }
    }
}