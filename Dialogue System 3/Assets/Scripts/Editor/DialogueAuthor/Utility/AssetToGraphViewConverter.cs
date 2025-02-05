using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using VirtualDeviants.Editor.DialogueAuthor.Graph;

namespace VirtualDeviants.Editor.DialogueAuthor.Utility {
    public static class AssetToGraphViewConverter {
        public static DialogueGraphView Convert(GraphAsset graphAsset, EditorWindow window) {
            DialogueGraphView graphView = new DialogueGraphView(window);

            GraphNode[] nodes = CreateGraphNodes(graphAsset);
            
            AddNodesToGraph(nodes, graphAsset.nodes, graphView);
            ConnectGraphNodes(nodes, graphAsset.nodes, graphView);
            GroupGraphNodes(nodes, graphAsset.groups, graphView);
			
            return graphView;
        }
        
        private static void GroupGraphNodes(GraphNode[] nodes, SerializedGraphGroup[] groups, DialogueGraphView graphView) {
            if (groups == null || nodes == null) {
                return;
            }
            
            foreach (SerializedGraphGroup group in groups) {
                GraphNode[] containedNodes = group.containedNodes.Select(node => nodes[node]).ToArray();
                graphView.AddGroupToGraph(group.groupTitle, containedNodes);
            }
        }

        private static void ConnectGraphNodes(
            GraphNode[] graphNodes, 
            SerializedGraphNode[] serializedNodes, 
            DialogueGraphView graphView
        ) {
            for (int i = 0; i < graphNodes.Length; i++) {
                GraphNode[] connections = serializedNodes[i].connections
                    .Select(connection => connection == -1 ? null : graphNodes[connection])
                    .ToArray();

                Port[] ports = graphNodes[i].GetOutputPorts();

                for (int j = 0; j < connections.Length; j++) {
                    if (connections[j] == null) {
                        continue;
                    }

                    Port outputPort = ports[j];
                    Port inputPort = connections[j].GetInputPort();
                    
                    graphView.CreateConnection(outputPort, inputPort);
                }
            }
        }
        
        private static void AddNodesToGraph(
            GraphNode[] graphNodes, 
            SerializedGraphNode[] serializedNodes, 
            DialogueGraphView graphView
        ) {
            for (int i = 0; i < graphNodes.Length; i++) {
                graphView.AddNodeToGraph(graphNodes[i], serializedNodes[i].position);
            }
        }

        private static GraphNode[] CreateGraphNodes(GraphAsset asset) {
            if (asset.nodes == null) {
                return Array.Empty<GraphNode>();
            }
            
            return asset.nodes.Select(serializedNode => new GraphNode(serializedNode.template)).ToArray();
        }
    }
}
